using System;
using DataAccess.Data;
using BusinessLogic.DTOs;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using DataAccess.Repositories.Implementations;
using Microsoft.Extensions.Caching.Memory;
using BusinessLogic.Configurations;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLogic.BL;

public class CategoryManagement
{
    // // ! Now after making the Repository Pattern .. We need to learn how to use AutoMapper (DTOs <-> Models) so we can remove the context and make BL only responsible for business logic not Database operations too
    // private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache _cache;
    private readonly string cacheKey = "Categories";
    public CategoryManagement(IUnitOfWork unitOfWork, IMemoryCache cache)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    public Category? Find(int? id) => _unitOfWork.CategoryRepository.GetById(id);

    public IEnumerable<CategoryDTO> GetCategories()
    {
        if (_cache.TryGetValue(cacheKey, out IEnumerable<CategoryDTO>? Categories))
        {
            return Categories!;
        }

        Categories = _unitOfWork.CategoryRepository.All().Select(c => new CategoryDTO
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            CreatedAt = c.CreatedAt.ToString("dd MMM yyyy")
        });
        _cache.Set(cacheKey, Categories, MemoryCacheConfig.Configuration());

        return Categories;
    }

    public IEnumerable<CategoryLookUpDto> GetCategoriesLookUp()
    {
        /* if (_cache.TryGetValue(cacheKey, out IEnumerable<Category>? Categories))
        {
            var CategoriesLookUp = Categories.Adapt<IEnumerable<CategoryLookUpDto>>();
            return CategoriesLookUp.Select(x => new CategoryLookUpDto { Id = x.Id, Name = x.Name }).ToList();
        } */

        var categories = GetCategories();
        // return categories.Select(x => new CategoryLookUpDto { Id = x.Id, Name = x.Name }).ToList();
        return categories.Adapt<IEnumerable<CategoryLookUpDto>>();
    }

    public Category? GetCategoryById(int? Id) => _unitOfWork.CategoryRepository.GetById(Id);

    public async Task<IEnumerable<ArchivedCategoriesDTO>> GetArchivedCategoriesAsync()
    {
        var archivedCategoriesRaw = await _unitOfWork.CategoryRepository.GetArchivedCategories();
        // var archivedCategories = archivedCategoriesRaw.Adapt<IEnumerable<ArchivedCategoriesDTO>>();
        var archivedCategories = archivedCategoriesRaw.Select(c => new ArchivedCategoriesDTO
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            CreatedAt = c.CreatedAt.ToString("dd MMM yyyy")
        });
        return archivedCategories;
    }

    public bool CreateCategory(Category category)
    {
        try
        {
            _unitOfWork.CategoryRepository.Create(category);
            _unitOfWork.Save();

            ClearCache();
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public bool UpdateCategory(Category category)
    {
        try
        {
            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Save();

            ClearCache();
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public bool DeleteCategory(int? id)
    {
        try
        {
            // Hard Delete
            /* var categoryToDelete = _unitOfWork.CategoryRepository.GetById(id);
            if (categoryToDelete is null)
                return false;
            _unitOfWork.CategoryRepository.Delete(id);
            _unitOfWork.Save(); */

            // Soft Delete
            var category = _unitOfWork.CategoryRepository.GetById(id);
            if (category is null)
                return false;

            category.IsDeleted = true;
            category.DeletedAt = DateTime.UtcNow;

            //* Ensuring that the Change tracker recognize it !
            _unitOfWork.CategoryRepository.Update(category);

            _unitOfWork.Save();

            ClearCache();
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> RestoreCategoryAsync(int? id)
    {
        try
        {
            var deletedCategory = await _unitOfWork.CategoryRepository.GetWithIgnoreFiltersAsync(c => c.Id == id);
            if (deletedCategory is null)
                return false;

            deletedCategory.IsDeleted = false;

            _unitOfWork.CategoryRepository.Update(deletedCategory);

            _unitOfWork.Save();

            ClearCache();

            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Message: {ex.Message}");
            return false;
        }
    }

    #region Helper Methods
    private void ClearCache()
    {
        _cache.Remove(cacheKey);
    }
    /* ! Not Efficient : because it doesn't apply the Cache-Aside Pattern (which is call the cache when you just need it, مش علي الفاضي والمليان)
    private void UpdateCache()
    {
        // Remove the old cache and Set the New one
        ClearCache();
        _cache.Set(cacheKey, GetCategories(), MemoryCacheConfig.Configuration());
        //^ The Cache will contain `IEnumerable<Category>` stored in it
    } */
    #endregion
}

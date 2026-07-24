using System;
using DataAccess.Data;
using BusinessLogic.DTOs;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using DataAccess.Repositories.Implementations;
using Microsoft.Extensions.Caching.Memory;
using BusinessLogic.Configurations;
using Mapster;

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

    public IEnumerable<Category> GetCategories()
    {
        if (_cache.TryGetValue(cacheKey, out IEnumerable<Category>? Categories))
        {
            return Categories!;
        }

        Categories = _unitOfWork.CategoryRepository.All();
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
            // _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            // var categoryToDelete = _unitOfWork.CategoryRepository.GetById(id);
            _unitOfWork.CategoryRepository.Delete(id);
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
    public Category? GetCategoryById(int? Id) => _unitOfWork.CategoryRepository.GetById(Id);

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

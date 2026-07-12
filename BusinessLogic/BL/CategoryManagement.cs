using System;
using DataAccess.Data;
using BusinessLogic.DTOs;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using DataAccess.Repositories.Implementations;

namespace BusinessLogic.BL;

public class CategoryManagement
{
    // ! Now after making the Repository Pattern .. We need to learn how to use AutoMapper (DTOs <-> Models) so we can remove the context and make BL only responsible for business logic not Database operations too
    // private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    public CategoryManagement(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Category? Find(int? id) => _unitOfWork.CategoryRepository.GetById(id);

    public IEnumerable<Category> GetCategories() => _unitOfWork.CategoryRepository.All();

    public IEnumerable<CategoryLookUpDto> GetCategoriesLookUp()
    {
        return _unitOfWork.CategoryRepository.All().Select(x => new CategoryLookUpDto { Id = x.Id, Name = x.Name }).ToList();
    }

    public bool CreateCategory(Category category)
    {
        try
        {
            _unitOfWork.CategoryRepository.Create(category);
            _unitOfWork.Save();
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
            return true;
        }
        catch(Exception ex)
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
            return true;
        }
        catch(Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            return false;
        }
    }
    public Category? GetCategoryById(int? Id) => _unitOfWork.CategoryRepository.GetById(Id);
}

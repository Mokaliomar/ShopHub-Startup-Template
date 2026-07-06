using System;
using DataAccess.Data;
using BusinessLogic.DTOs;
using DataAccess.Models;

namespace BusinessLogic.BL;

public class CategoryManagement
{
    private readonly ApplicationDbContext _context;
    public CategoryManagement(ApplicationDbContext context)
    {
        _context = context;
    }

    public Category? Find(int? id) => _context.Categories.Find(id);

    public IEnumerable<Category> GetCategories() => _context.Categories.ToList();

    public IEnumerable<CategoryLookUpDto> GetCategoriesLookUp()
    {
        return _context.Categories.Select(x => new CategoryLookUpDto { Id = x.Id, Name = x.Name }).ToList();
    }

    public bool CreateCategory(Category category)
    {
        try
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
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
            _context.Categories.Update(category);
            _context.SaveChanges();
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
            var categoryToDelete = _context.Categories.FirstOrDefault(x => x.Id == id);
            _context.Categories.Remove(categoryToDelete);
            _context.SaveChanges();
            return true;
        }
        catch(Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            return false;
        }
    }
    public Category? GetCategoryById(int? Id) => _context.Categories.FirstOrDefault(c => c.Id == Id);
}

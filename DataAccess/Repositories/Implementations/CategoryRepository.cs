using System;
using DataAccess.Data;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Implementations;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    // private readonly ApplicationDbContext _context;
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {}
    
    //! If you will not edit the method in the parent class, so you will not need to override, In our case we just are testing
    public override IEnumerable<Category> All()
    {
        return dbSet.ToList();
    }

/* Without GenericRepository
    public IEnumerable<Category> GetCategories() => _context.Categories;
    public Category GetCategoryById(int? Id) => _context.Categories.FirstOrDefault(c => c.Id == Id);
    public void CreateCategory(Category category) => _context.Categories.Add(category);
    public void UpdateCategory(Category category) => _context.Categories.Update(category);
    public void DeleteCategory(int Id)
    {
        var category = _context.Categories.FirstOrDefault(c => c.Id == Id);
        _context.Categories.Remove(category);
    }
 */
}

using System;
using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces;

public interface ICategoryRepository
{
    IEnumerable<Category> GetCategories();
    Category GetCategoryById(int? Id);
    void CreateCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(int Id);
}

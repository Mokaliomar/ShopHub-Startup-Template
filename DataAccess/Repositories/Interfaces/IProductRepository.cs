using System;
using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    IEnumerable<Product> GetProductsWithCategories();
    Product? GetProductWithCategory(int Id);
}

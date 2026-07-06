using System;
using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces;

public interface IProductRepository
{
    IEnumerable<Product> GetProducts();
    IEnumerable<Product> GetProductsWithCategories();
    Product GetProductById(int? Id);
    void CreateProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(int Id);
}

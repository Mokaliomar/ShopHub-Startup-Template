using System;
using DataAccess.Data;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetProducts() => _context.Products;
    public IEnumerable<Product> GetProductsWithCategories() => _context.Products.Include(p => p.Category);
    public Product? GetProductById(int? Id) => _context.Products.FirstOrDefault(p => p.Id == Id);
    public void CreateProduct(Product product) => _context.Products.Add(product);
    public void UpdateProduct(Product product) => _context.Products.Update(product);
    public void DeleteProduct(int Id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == Id);
        _context.Products.Remove(product);
    }
}

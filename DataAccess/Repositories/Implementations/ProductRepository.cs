using System;
using DataAccess.Data;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {}

    public IEnumerable<Product> GetProductsWithCategories()
    {
        return dbSet.Include(x => x.Category);
    }


    /* Without GenericRepository
    public IEnumerable<Product> GetProducts() => _context.Products;
    public IEnumerable<Product> GetProductsWithCategories() => _context.Products.Include(p => p.Category);
    public Product? GetProductById(int? Id) => _context.Products.FirstOrDefault(p => p.Id == Id);
    public void CreateProduct(Product product) => _context.Products.Add(product);
    public void UpdateProduct(Product product) => _context.Products.Update(product);
    public void DeleteProduct(int Id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == Id);
        _context.Products.Remove(product);
    } */
}

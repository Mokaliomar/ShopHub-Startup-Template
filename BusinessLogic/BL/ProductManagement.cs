using System;
using BusinessLogic.Data;
using BusinessLogic.DTOs;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.BL;

public class ProductManagement
{
    private readonly ApplicationDbContext _context;
    public ProductManagement(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ProductDto> GetProducts()
    {
        return _context.Products.Include(x => x.Category)
               .Select(x => new ProductDto
               {
                   Id = x.Id,
                   Name = x.Name,
                   Description = x.Description,
                   Price = x.Price,
                   CategoryName = x.Category.Name
               }).ToList();
    }

    public bool UpsertProduct(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        try
        {
            if (product.Id == 0)
            {
                _context.Products.Add(product);
            }
            else // Update Product
            {
                //! Need a validation if someone used a Postman to update a product
                _context.Products.Update(product);
            }
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public bool DeleteProduct(int Id)
    {
        try
        {
            var product = GetProductById(Id);
            if (product == null)
                return false;
            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }
        catch(Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            return false;
        }
    }
    public bool DeleteProduct(Product product)
    {
        try
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }
        catch(Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            return false;
        }
    }
    public Product? GetProductById(int? Id) => _context.Products.FirstOrDefault(p => p.Id == Id);
}

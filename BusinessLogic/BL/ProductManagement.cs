using System;
using DataAccess.Data;
using BusinessLogic.DTOs;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using DataAccess.Repositories.Interfaces;

namespace BusinessLogic.BL;

public class ProductManagement
{
    // private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    public ProductManagement(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<ProductDto> GetProductsWithCategories()
    {
        return _unitOfWork.ProductRepository.GetProductsWithCategories()
               .Select(x => new ProductDto
               {
                   Id = x.Id,
                   Image = x.Img,
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
                _unitOfWork.ProductRepository.Create(product);
            }
            else // Update Product
            {
                //! Need a validation if someone used a Postman to update a product
                _unitOfWork.ProductRepository.Update(product);
            }
            _unitOfWork.Save();
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public bool DeleteProduct(int? Id)
    {
        try
        {
            var product = GetProductWithCategoryById(Id);
            if (product == null)
                return false;
            _unitOfWork.ProductRepository.Delete(Id);
            _unitOfWork.Save();
            return true;
        }
        catch(Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            return false;
        }
    }
    /* public bool DeleteProduct(Product product)
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
    } */
    public Product? GetProductWithCategoryById(int? Id) => _unitOfWork.ProductRepository.GetProductsWithCategories().FirstOrDefault(p => p.Id == Id);
    
    public Product? GetProductById(int? Id) => _unitOfWork.ProductRepository.GetById(Id);
}

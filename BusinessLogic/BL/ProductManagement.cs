using System;
using DataAccess.Data;
using BusinessLogic.DTOs;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using DataAccess.Repositories.Interfaces;
using Mapster;

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
        var result = _unitOfWork.ProductRepository.GetProductsWithCategories()
               .Select(x => new ProductDto
               {
                   Id = x.Id,
                   Image = x.Img,
                   Name = x.Name,
                   Description = x.Description,
                   Price = x.Price,
                   CategoryName = x.Category.Name
               });
        return result;
    }
    public async Task<IEnumerable<ArchivedProductsDTO>> GetArchivedProducts()
    {
        var archivedProductsRaw = await _unitOfWork.ProductRepository
                                    .GetArchivedProductsAsync();
        /* .Select(p => new ArchivedProductsDTO
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            CategoryName = p.Category.Name
        }); */

        var archivedProducts = archivedProductsRaw.Adapt<IEnumerable<ArchivedProductsDTO>>();

        return archivedProducts;
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
            /* Hard Delete
            var product = GetProductWithCategoryById(Id);
            if (product == null)
                return false;
            _unitOfWork.ProductRepository.Delete(Id);
            _unitOfWork.Save();
            return true; */

            // Soft Delete
            var product = GetProductWithCategoryById(Id);
            if (product == null)
                return false;

            product.IsDeleted = true;
            product.DeletedAt = DateTime.UtcNow;

            //* Ensuring that it will be saved in the Change Tracker (to avoid the Untracked entity)
            _unitOfWork.ProductRepository.Update(product);


            _unitOfWork.Save();

            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            return false;
        }
    }
    public async Task<bool> RestoreProductAsync(int? id)
    {
        try
        {
            var deletedProduct = await _unitOfWork.ProductRepository.GetWithIgnoreFiltersAsync(p => p.Id == id);
            if (deletedProduct is null)
                return false;

            deletedProduct.IsDeleted = false;

            _unitOfWork.ProductRepository.Update(deletedProduct);

            _unitOfWork.Save();

            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public Product? GetProductWithCategoryById(int? Id) => _unitOfWork.ProductRepository.GetProductsWithCategories().FirstOrDefault(p => p.Id == Id);

    public Product? GetProductById(int? Id) => _unitOfWork.ProductRepository.GetById(Id);

    public ProductListDto GetPaginatedProducts(string? searchTerm, string? sortingTerm, int pageNumber = 1, int pageSize = 10)
    {
        var query = ApplySearchAndSort(searchTerm, sortingTerm);

        var totalItems = query.Count();
        var numberOfPages = (int)Math.Ceiling((float)totalItems / pageSize);

        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        /* #region Mapster Configurations
        TypeAdapterConfig<Product, ProductDto>.NewConfig()
            .Map(dest => dest.Image, src => src.Img);
        TypeAdapterConfig<Product, ProductDto>.NewConfig()
            .Map(dest => dest.CategoryName, src => src.Category.Name);
        #endregion */

        var products = query.ProjectToType<ProductDto>();

        var productListDto = new ProductListDto
        {
            Items = products,
            PageIndex = pageNumber,
            TotalPages = numberOfPages,
            SearchTerm = searchTerm,
            SortingTerm = sortingTerm,
        };

        return productListDto;
    }
    private IQueryable<Product> ApplySearchAndSort(string? searchTerm, string? sortingTerm)
    {
        var query = _unitOfWork.ProductRepository.GetProductsWithCategories().AsQueryable();

        //* Searching
        if (!string.IsNullOrEmpty(searchTerm))
            query = query.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));

        //* Sorting
        switch (sortingTerm)
        {
            case "NameAsc":
                query = query.OrderBy(p => p.Name);
                break;

            case "NameDesc":
                query = query.OrderByDescending(p => p.Name);
                break;

            case "PriceAsc":
                query = query.OrderBy(p => p.Price);
                break;

            case "PriceDesc":
                query = query.OrderByDescending(p => p.Price);
                break;
        }
        return query;
    }

}

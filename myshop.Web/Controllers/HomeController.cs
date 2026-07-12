using BusinessLogic.BL;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using myshop.Entities.ViewModels;
using myshop.Web.ViewModels;

namespace myshop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductManagement _productManagement;
        public HomeController(ProductManagement productManagement)
        {
            _productManagement = productManagement;
        }
        // GET: HomeController
        public IActionResult Index()
        {
            var productsWithCategories = _productManagement.GetProductsWithCategories();
            IEnumerable<ProductShopIndexVM> product = productsWithCategories.Select(x => new ProductShopIndexVM
            {
                Id = x.Id,
                Image = x.Image,
                CategoryName = x.CategoryName,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
            });

            return View(product);
        }

        public IActionResult Details(int? id)
        {
            /* var productVM = new ProductVM()
            {
                Product = _productManagement.GetProductWithCategoryById(id),
            }; */

            var theProduct = _productManagement.GetProductWithCategoryById(id);
            var product = new ProductShopIndexVM()
            {
                Id = theProduct.Id,
                Image = theProduct.Img,
                CategoryName = theProduct.Category.Name,
                Name = theProduct.Name,
                Description = theProduct.Description,
                Price = theProduct.Price,
            };

            return View(product);
        }
    }
}

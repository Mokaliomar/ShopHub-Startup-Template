using BusinessLogic.BL;
using BusinessLogic.DTOs;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myshop.Entities.ViewModels;
using myshop.Web.ViewModels;

namespace myshop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductManagement _productManagement;
        private readonly ReviewManagement _reviewManagement;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ProductManagement productManagement, UserManager<ApplicationUser> userManager, ReviewManagement reviewManagement)
        {
            _productManagement = productManagement;
            _userManager = userManager;
            _reviewManagement = reviewManagement;
        }
        // GET: HomeController
        public IActionResult Index(string? searchTerm, string? sortingTerm, int pageNumber = 1)
        {
            /* var productsWithCategories = _productManagement.GetProductsWithCategories();
            IEnumerable<ProductShopIndexVM> product = productsWithCategories.Select(x => new ProductShopIndexVM
            {
                Id = x.Id,
                Image = x.Image,
                CategoryName = x.CategoryName,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
            }); */
            var productListDto = _productManagement.GetPaginatedProducts(searchTerm, sortingTerm, pageNumber, 8);
            var productListVM = productListDto.Adapt<ProductListVM>();

            return View(productListVM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null)
                return RedirectToAction("Login", "Account");

            var theProduct = _productManagement.GetProductWithCategoryById(id);
            var product = new ProductShopIndexVM()
            {
                Id = theProduct.Id,
                Image = theProduct.Img,
                CategoryName = theProduct.Category.Name,
                Name = theProduct.Name,
                Description = theProduct.Description,
                Price = theProduct.Price,

                AverageRate = _reviewManagement.GetAvgProductReviewRate(id),
                ReviewsCount = _reviewManagement.GetProductReviewsCount(id),
                Reviews = _reviewManagement.GetProductReviews(id).Select(r => new ReviewVM()
                {
                    UserId = user.Id,
                    UserImg = "",
                    UserName = user.Name,
                    CreationDate = r.CreatedAt,
                    Rate = r.ProductRate
                })
            };

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(Review review)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null)
                return RedirectToAction("Login", "Account");
            review.ApplicationUserId = user.Id;
            _reviewManagement.AddReview(review);
            return RedirectToAction("Details", new { id = review.ProductId });
        }
    }
}

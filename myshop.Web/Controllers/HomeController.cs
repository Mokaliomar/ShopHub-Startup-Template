using System.Diagnostics.Eventing.Reader;
using BusinessLogic.BL;
using BusinessLogic.DTOs;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myshop.Entities.ViewModels;
using myshop.Web.ViewModels;
using NuGet.Protocol.Core.Types;

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
                    Id = r.Id,
                    UserId = r.ApplicationUserId,
                    UserImg = "",
                    UserName = r.ApplicationUser.Name,
                    CreationDate = r.CreatedAt,
                    TheReview = r.TheReview,
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

            if (_reviewManagement.HasReview(review.ApplicationUserId, review.ProductId))
            {
                TempData["DuplicateReview"] = "Can't make more than one review !";
                return RedirectToAction("Details", new { id = review.ProductId });
            }

            _reviewManagement.AddReview(review); // needs a check if the user tries to add another review
            return RedirectToAction("Details", new { id = review.ProductId });
        }

        [HttpGet]
        public async Task<IActionResult> EditReview(int reviewId)
        {
            var review = _reviewManagement.GetReviewById(reviewId);
            var isAuthorized = await IsUserAuthorizedForReview(review);
            if (!isAuthorized)
            {
                return Forbid();
            }

            var theReview = _reviewManagement.GetReviewById(reviewId);
            return View(theReview);
        }

        [HttpPost]
        public async Task<IActionResult> EditReview(Review review)
        {
            var isAuthorized = await IsUserAuthorizedForReview(review);
            if (!isAuthorized)
                return Forbid();

            bool isEdited = _reviewManagement.EditReview(review);
            if (isEdited)
                return RedirectToAction("Details", new { id = review.ProductId });

            return View(review);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveReview(int reviewId)
        {
            var review = _reviewManagement.GetReviewById(reviewId);
            var isAuthorized = await IsUserAuthorizedForReview(review);
            if (!isAuthorized)
            {
                return Forbid();
            }

            _reviewManagement.RemoveReview(reviewId);
            return RedirectToAction("Details", new { id = review.ProductId });
        }

        private async Task<bool> IsUserAuthorizedForReview(Review? review)
        {
            if (review == null)
                return false;

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return false;

            // هل المستخدم الحالي هو صاحب المراجعة؟
            return review.ApplicationUserId == user.Id;
        }
    }
}

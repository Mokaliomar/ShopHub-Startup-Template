using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using myshop.Entities.ViewModels;

namespace myshop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: HomeController
        public IActionResult Index()
        {
            var productsWithCategories = _unitOfWork.ProductRepository.GetProductsWithCategories();
            IEnumerable<ProductVM> productVM = productsWithCategories.Select(p => new ProductVM
            {
                Product = p
            });


            return View(productVM);
        }

        public IActionResult Details(int? id)
        {
            var productVM = new ProductVM()
            {
                Product = _unitOfWork.ProductRepository.GetProductsWithCategories().FirstOrDefault(p => p.Id == id)
            };

            return View(productVM);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using DataAccess.Data;
using DataAccess.Models;
using myshop.Entities.ViewModels;
using BusinessLogic.BL;

namespace myshop.Web.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // private readonly ApplicationDbContext _context;
        private readonly ProductManagement _productManagement;
        private readonly CategoryManagement _categoryManagement;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ProductManagement productManagement, CategoryManagement categoryManagement, IWebHostEnvironment webHostEnvironment)
        {
            _productManagement = productManagement;
            _categoryManagement = categoryManagement;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetData()
        {
            var products = _productManagement.GetProducts().ToList();

            //?? What does this return type does ????
            return Json(new { data = products });
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _categoryManagement.GetCategoriesLookUp().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
            };
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string RootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(RootPath, @"Images\Products");
                    var ext = Path.GetExtension(file.FileName);

                    using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    productVM.Product.Img = @"Images\Products\" + filename + ext;
                }

                bool isAdded = _productManagement.UpsertProduct(productVM.Product);
                if (isAdded)
                {
                    TempData["Create"] = "Item has Created Successfully";
                    return RedirectToAction("Index");
                }
            }
            // إصلاح الخطأ: إعادة تعبئة القائمة وإرجاع الـ productVM بالكامل وليس الـ Product فقط
            productVM.CategoryList = _categoryManagement.GetCategoriesLookUp().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            return View(productVM);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            ProductVM productVM = new ProductVM()
            {
                Product = _productManagement.GetProductById(id),
                CategoryList = _categoryManagement.GetCategoriesLookUp().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Edit(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string RootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(RootPath, @"Images\Products");
                    var ext = Path.GetExtension(file.FileName);

                    if (productVM.Product.Img != null)
                    {
                        var oldimg = Path.Combine(RootPath, productVM.Product.Img.TrimStart('\\'));

                        if (System.IO.File.Exists(oldimg))
                        {
                            System.IO.File.Delete(oldimg);
                        }
                    }

                    using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }

                    productVM.Product.Img = @"Images\Products\" + filename + ext;
                }

                bool isUpdated = _productManagement.UpsertProduct(productVM.Product);
                if (isUpdated)
                {
                    TempData["Update"] = "Data has Updated Successfully";
                    return RedirectToAction("Index");
                }
            }

            return View(productVM.Product);
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productIndb = _productManagement.GetProductById(id);

            if (productIndb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            //! NOT FINISHED
            bool isDeleted = _productManagement.DeleteProduct(productIndb);

            var oldimg = Path.Combine(_webHostEnvironment.WebRootPath, productIndb.Img.TrimStart('\\'));

            if (System.IO.File.Exists(oldimg))
            {
                System.IO.File.Delete(oldimg);
            }

            // _context.SaveChanges();

            return Json(new { success = true, message = "file has been Deleted" });
        }


    }
}

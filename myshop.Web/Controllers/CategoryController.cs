using Microsoft.AspNetCore.Mvc;
using DataAccess.Data;
using DataAccess.Models;
using BusinessLogic.BL;
using Microsoft.AspNetCore.Authorization;

// namespace myshop.Web.Areas.Admin.Controllers
namespace myshop.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        // private readonly ApplicationDbContext _context;
        private readonly CategoryManagement _categoryManagement;

        public CategoryController(CategoryManagement categoryManagement)
        {
            _categoryManagement = categoryManagement;
        }

        public IActionResult Index()
        {
            var categories = _categoryManagement.GetCategories().ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var isCreated = _categoryManagement.CreateCategory(category);
                if (isCreated)
                {
                    TempData["Create"] = "Item has Created Successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null | id == 0)
            {
                NotFound();
            }
            // ! I'm not sure if this code is correct
            var categoryIndb = _categoryManagement.Find(id);
            // Original:- var categoryIndb = _context.Categories.Find(id);

            return View(categoryIndb);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _categoryManagement.UpdateCategory(category);
                if (isUpdated)
                {
                    TempData["Update"] = "Data has Updated Successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null | id == 0)
            {
                NotFound();
            }
            var categoryIndb = _categoryManagement.GetCategoryById(id);

            return View(categoryIndb);
        }

        [HttpPost]
        public IActionResult DeleteCategory(int? id)
        {
            // var categoryIndb = _context.Categories.FirstOrDefault(x => x.Id == id);
            var categoryIndb = _categoryManagement.GetCategoryById(id);
            if (categoryIndb == null)
            {
                NotFound();
            }
            // _context.Categories.Remove(categoryIndb);
            // _context.SaveChanges();
            var isDeleted = _categoryManagement.DeleteCategory(id);
            if (isDeleted)
            {
                TempData["Delete"] = "Item has Deleted Successfully";
                return RedirectToAction("Index");
            }
            //! NOT SURE
            return RedirectToAction("Edit");
        }
    }
}

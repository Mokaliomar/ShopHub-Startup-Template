using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myshop.Web.ViewModels;

namespace myshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            List<UserVM> usersVM = [];
            var users = await _userManager.Users.ToListAsync();
            foreach(var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                usersVM.Add(new UserVM
                {
                    Id = user.Id,
                    Username = user.Name,
                    Email = user.Email,
                    Role = roles.FirstOrDefault() ?? "Customer",
                });
            }
            return View(usersVM);
        }
    }
}


using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myshop.Web.ViewModels;

namespace myshop.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Login Section
        // GET: UserController
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl)
        {
            return View(new LoginVM()
            {
                ReturnUrl = ReturnUrl
            });
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM model, string? returnUrl)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(model);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

            if (signInResult.Succeeded)
            {
                if (string.IsNullOrEmpty(returnUrl))
                {
                    // return RedirectToAction("Index", "Product");
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(returnUrl);
            }

            ModelState.AddModelError(string.Empty, "Something Went wrong !");
            return View(model);
        }

        #endregion

        #region Register Section

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            var user = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                City = model.City,
                Address = model.Address,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //* Adding the User Role to "Customer" By default
                await _userManager.AddToRoleAsync(user, "Customer");

                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
                if (signInResult.Succeeded)
                {
                    // return RedirectToAction("Index", "Product");
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError(string.Empty, "Something Went wrong !");
            return View(model);
        }

        #endregion

        #region Logout Section

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        #endregion
    }
}

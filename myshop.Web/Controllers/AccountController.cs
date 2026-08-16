using BusinessLogic.Configurations;
using BusinessLogic.DTOs;
using BusinessLogic.Services.Implementation;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using myshop.Web.ViewModels;
using Org.BouncyCastle.Asn1.X509;

namespace myshop.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
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

                //* Making an Email Confirmation Message (before signing in)
                var theToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(
                    action: nameof(EmailConfirmation),
                    controller: "Account",
                    values: new { userId = user.Id, token = theToken },
                    protocol: Request.Scheme
                );
                await _emailService.CreateEmailConfirmationMessageAsync(user.UserName, user.Email, confirmationLink, DateTime.Now.Year.ToString());

                return RedirectToAction("CheckYourEmail");
            }

            ModelState.AddModelError(string.Empty, "Something Went wrong !");
            return View(model);
        }

        public IActionResult CheckYourEmail()
        {
            return View();
        }

        // [HttpGet("{userId}/{token}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> EmailConfirmation(string userId, string token)
        {
            //* Checking the UserId & the Token Validation
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return View();

            var identityResult = await _userManager.ConfirmEmailAsync(user, token);

            if (!identityResult.Succeeded)
            {
                // message = "Error: " + identityResult.Errors;
                return RedirectToAction("Register");
            }

            //* Signing In After checking that he confirmed his email !
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("Index", "Home");
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

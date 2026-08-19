using BusinessLogic.BL;
using BusinessLogic.DTOs;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using myshop.Web.ViewModels;
using Newtonsoft.Json;

namespace myshop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ProductManagement _productManagement;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly ICartService _cartService;
        public OrderController(ICartService cartService, ProductManagement productManagement, UserManager<ApplicationUser> userManager, IEmailService emailService)
        {
            _productManagement = productManagement;
            _userManager = userManager;
            _emailService = emailService;
            _cartService = cartService;
        }

        public IActionResult Cart()
        {
            /* ShoppingCartVM? shoppingCart = new();
            if (HttpContext.Session.GetString("Cart") is not null)
                shoppingCart = JsonConvert.DeserializeObject<ShoppingCartVM>(HttpContext.Session.GetString("Cart")); */
            var shoppingCart = GetShoppingCart();
            return View(shoppingCart);
        }

        public IActionResult AddToCart(int id)
        {
            var shoppingCart = GetShoppingCart();

            // // * Testing the Mapster -> Worked So good !
            // Mapping the Object
            ShoppingCartDto? shoppingCartDto = shoppingCart.Adapt<ShoppingCartDto>();
            shoppingCartDto = _cartService.AddItem(shoppingCartDto, id);
            // shoppingCartDto.
            shoppingCart = shoppingCartDto.Adapt<ShoppingCartVM>();

            SaveShoppingCartToSession(shoppingCart);

            return RedirectToAction("Index", "Home");
            // return RedirectToAction(nameof(Cart));
        }

        public IActionResult QuantityPlus(int productId)
        {
            var shoppingCart = GetShoppingCart()!;

            /* // //! This logic need to be modified !!!!!
            if (shoppingCart is null || shoppingCart.CartItems is null || shoppingCart.CartItems.Count == 0)
            {
                //! Add ModelState Error message
                return RedirectToAction(nameof(Cart));
            } */

            // Mapping the Object
            ShoppingCartDto? shoppingCartDto = shoppingCart.Adapt<ShoppingCartDto>();
            shoppingCartDto = _cartService.IncreaseQuantity(shoppingCartDto, productId);
            shoppingCart = shoppingCartDto.Adapt<ShoppingCartVM>();


            SaveShoppingCartToSession(shoppingCart);

            return RedirectToAction(nameof(Cart));
        }
        public IActionResult QuantityMinus(int productId)
        {
            var shoppingCart = GetShoppingCart();
            if (shoppingCart is null)
                return RedirectToAction(nameof(Cart));

            // Mapping the Object
            var shoppingCartDto = shoppingCart.Adapt<ShoppingCartDto>();
            shoppingCartDto = _cartService.DecreaseQuantity(shoppingCartDto, productId);
            shoppingCart = shoppingCartDto.Adapt<ShoppingCartVM>();

            SaveShoppingCartToSession(shoppingCart);

            return RedirectToAction(nameof(Cart));
        }

        public ShoppingCartVM? GetShoppingCart()
        {
            var shoppingCartJson = HttpContext.Session.GetString("Cart");
            if (shoppingCartJson is null)
                return new();

            var shoppingCart = JsonConvert.DeserializeObject<ShoppingCartVM>(shoppingCartJson);
            return shoppingCart;
        }
        public void SaveShoppingCartToSession(ShoppingCartVM shoppingCart)
        {
            var shoppingCartJson = JsonConvert.SerializeObject(shoppingCart);
            HttpContext.Session.SetString("Cart", shoppingCartJson);
        }

        public IActionResult RemoveItem(int productId)
        {
            var shoppingCart = GetShoppingCart();
            if (shoppingCart is null)
                return RedirectToAction("Cart");

            /* var itemToRemove = shoppingCart.CartItems.FirstOrDefault(item => item.Id == productId)!;
            shoppingCart.CartItems.Remove(itemToRemove); */

            // Mapping the Objects
            var shoppingCartDto = shoppingCart.Adapt<ShoppingCartDto>();
            shoppingCartDto = _cartService.RemoveItem(shoppingCartDto, productId);
            shoppingCart = shoppingCartDto.Adapt<ShoppingCartVM>();

            SaveShoppingCartToSession(shoppingCart);
            return RedirectToAction("Cart");
        }

        public IActionResult ClearCart()
        {
            var shoppingCart = GetShoppingCart()!;

            // Mapping the Objects
            var shoppingCartDto = shoppingCart.Adapt<ShoppingCartDto>();
            shoppingCartDto = _cartService.ClearCart(shoppingCartDto);
            shoppingCart = shoppingCartDto.Adapt<ShoppingCartVM>();

            SaveShoppingCartToSession(shoppingCart);
            return RedirectToAction("Cart");
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> OrderSuccess(ShoppingCartVM checkoutOrder)
        {
            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            OrderConfirmationDTO order = new()
            {
                CustomerName = user.Name,
                OrderId = 1,
                OrderItems = checkoutOrder.CartItems.Adapt<List<CartItemDto>>(),
                TotalPrice = checkoutOrder.Total,
                ShippingAddress = user.Address,
                City = user.City,
                PhoneNumber = user.PhoneNumber
            };

            // await _emailService.CreateOrderConfirmationEmail(user.Name, user.Email, order);

            return View();
        }
    }
}
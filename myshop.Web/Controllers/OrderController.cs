using BusinessLogic.BL;
using BusinessLogic.DTOs;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models;
using Mapster;
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
        private readonly ICartService _cartService;
        public OrderController(ICartService cartService, ProductManagement productManagement)
        {
            _productManagement = productManagement;
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

    }
}
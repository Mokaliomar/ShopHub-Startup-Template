using BusinessLogic.BL;
using BusinessLogic.DTOs;
using BusinessLogic.Services.Interfaces;
using DataAccess.Enums;
using DataAccess.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using myshop.Web.ViewModels;
using Newtonsoft.Json;
using Stripe;

namespace myshop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ProductManagement _productManagement;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly OrderManagement _orderManagement;
        private readonly ICartService _cartService;

        public OrderController(ICartService cartService, ProductManagement productManagement, UserManager<ApplicationUser> userManager, IEmailService emailService, OrderManagement orderManagement)
        {
            _productManagement = productManagement;
            _userManager = userManager;
            _emailService = emailService;
            _orderManagement = orderManagement;
            _cartService = cartService;
        }

        #region Cart Actions
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

        #endregion

        #region Cart Helper Methods
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
        #endregion

        #region Checkout Process
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUserAsync(User);
            ShoppingCartVM shoppingCart = GetShoppingCart();

            var invoice = new InvoiceVM()
            {
                OrderHeader = new OrderHeader()
                {
                    // OrderInfo
                    TotalPrice = shoppingCart.Total,
                    OrderStatus = OrderStatus.Pending.ToString(),
                    PaymentStatus = PaymentStatus.Pending.ToString(),

                    // Customer Info
                    ApplicationUserId = user.Id,
                    Name = user.Name,
                    Address = user.Address,
                    City = user.City,
                    PhoneNumber = user.PhoneNumber,
                }
            };
            foreach (var item in shoppingCart.CartItems)
            {
                invoice.OrderDetails.Add(new OrderDetail()
                {
                    ProductId = item.Id,
                    Price = item.Price,
                    Product = new DataAccess.Models.Product
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Img = item.Img
                    },
                    Count = item.Quantity
                });
            }

            return View(invoice);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ProcessOrder(OrderHeader orderHeader)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (await _userManager.GetPhoneNumberAsync(user) == null)
            {
                await _userManager.SetPhoneNumberAsync(user, orderHeader.PhoneNumber);
            }

            ShoppingCartVM checkoutOrder = GetShoppingCart();
            if (checkoutOrder == null || checkoutOrder.CartItems == null || !checkoutOrder.CartItems.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            orderHeader.ApplicationUserId = user.Id;
            orderHeader.OrderDate = DateTime.UtcNow;
            orderHeader.OrderStatus = OrderStatus.Pending.ToString();
            orderHeader.PaymentStatus = PaymentStatus.Pending.ToString();
            orderHeader.TotalPrice = checkoutOrder.Total;

            var orderDetails = checkoutOrder.CartItems.Select(item => new OrderDetail()
            {
                // OrderHeaderId = orderHeader.Id,
                ProductId = item.Id,
                Price = item.Price,
                Count = item.Quantity,
            });

            _orderManagement.CreateOrder(orderHeader, orderDetails);

            //* Making the Order
            OrderConfirmationDTO order = new()
            {
                CustomerName = user.Name,
                OrderId = orderHeader.Id,
                OrderItems = checkoutOrder.CartItems.Adapt<List<CartItemDto>>(),
                TotalPrice = checkoutOrder.Total,
                ShippingAddress = orderHeader.Address,
                City = orderHeader.City,
                PhoneNumber = orderHeader.PhoneNumber,
            };

            //* Send the Order Confirmation Email ..
            await _emailService.CreateOrderConfirmationEmail(user.Name, user.Email, order);

            //* Clear the cart after the Purchase ..
            ShoppingCartVM shoppingCart = _cartService.ClearCart(checkoutOrder.Adapt<ShoppingCartDto>()).Adapt<ShoppingCartVM>();
            SaveShoppingCartToSession(shoppingCart);

            return RedirectToAction(nameof(OrderSuccess), new { orderId = orderHeader.Id });
        }

        public IActionResult OrderSuccess(int orderId)
        {
            return View(orderId);
        }

        #endregion

        [Authorize]
        public async Task<IActionResult> MyOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            
            if(user is null)
                return RedirectToAction("Login", "Account");

            var customerOrderRaw = _orderManagement.GetCustomerOrdersWithDetails(user.Id);

            var customerOrder = customerOrderRaw.Select(order => new MyOrdersVM()
            {
                OrderID = order.Id,
                OrderDate = order.OrderDate,
                
                ShippingDate = order.ShippingDate,
                
                OrderStatus = order.OrderStatus,
                PaymentStatus = order.PaymentStatus,
                
                OrderDetails = order.OrderDetails,
                
                TotalPrice = order.TotalPrice,
            });
            
            return View(customerOrder);
        }
    }
}
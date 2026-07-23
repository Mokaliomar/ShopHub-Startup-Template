using System;
using BusinessLogic.DTOs;


namespace BusinessLogic.Services.Interfaces;

public interface ICartService
{
    // List<CartItemDto> GetShoppingCart();

    // void AddItem(int productId, int quantity = 1);
    ShoppingCartDto AddItem(ShoppingCartDto? shoppingCart, int productId);

    ShoppingCartDto IncreaseQuantity(ShoppingCartDto shoppingCart, int productId);
    ShoppingCartDto DecreaseQuantity(ShoppingCartDto shoppingCart, int productId);

    ShoppingCartDto RemoveItem(ShoppingCartDto shoppingCart, int productId);

    ShoppingCartDto ClearCart(ShoppingCartDto shoppingCart);

    decimal GetTotal(ShoppingCartDto shoppingCart);
}

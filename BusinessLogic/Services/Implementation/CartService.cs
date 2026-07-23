using System;
using System.Net.Http.Json;
using BusinessLogic.BL;
using BusinessLogic.DTOs;
using BusinessLogic.Services.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Services.Implementation;

public class CartService : ICartService
{
    private readonly ProductManagement _productManagement;

    public CartService(ProductManagement productManagement)
    {
        _productManagement = productManagement;
    }

    public ShoppingCartDto AddItem(ShoppingCartDto? shoppingCart, int productId)
    {
        var newItem = _productManagement.GetProductById(productId);

        var newCartItem = newItem.Adapt<CartItemDto>();
        newCartItem!.Quantity = 1;

        var itemExists = shoppingCart?.CartItems.FirstOrDefault(c => c.Id == newCartItem!.Id);

        if (itemExists is not null)
            itemExists.Quantity++;
        else
            shoppingCart.CartItems.Add(newCartItem);

        //* Total Evaluation
        shoppingCart.Total = GetTotal(shoppingCart);

        return shoppingCart;
    }

    public ShoppingCartDto IncreaseQuantity(ShoppingCartDto shoppingCart, int productId)
    {
        var specificItem = _productManagement.GetProductById(productId)!;

        var cartItem = shoppingCart.CartItems.FirstOrDefault(item => item.Id == specificItem.Id)!;

        cartItem.Quantity++;

        // Recalculate the total 
        shoppingCart.Total = GetTotal(shoppingCart);

        return shoppingCart;
    }

    public ShoppingCartDto DecreaseQuantity(ShoppingCartDto shoppingCart, int productId)
    {
        var specificItem = _productManagement.GetProductById(productId)!;

        var cartItem = shoppingCart.CartItems.FirstOrDefault(item => item.Id == specificItem.Id)!;

        if (cartItem.Quantity - 1 > 0)
            cartItem.Quantity--;

        shoppingCart.Total = GetTotal(shoppingCart);

        return shoppingCart;
    }

    public decimal GetTotal(ShoppingCartDto shoppingCart)
    {
        return shoppingCart.CartItems.Sum(item => item.Price * item.Quantity);
    }

    public ShoppingCartDto RemoveItem(ShoppingCartDto shoppingCart, int productId)
    {
        var itemToRemove = shoppingCart.CartItems.FirstOrDefault(item => item.Id == productId)!;
        shoppingCart.CartItems.Remove(itemToRemove);

        shoppingCart.Total = GetTotal(shoppingCart);

        return shoppingCart;
    }
    public ShoppingCartDto ClearCart(ShoppingCartDto shoppingCart)
    {
        shoppingCart.CartItems.Clear();
        return shoppingCart;
    }
}


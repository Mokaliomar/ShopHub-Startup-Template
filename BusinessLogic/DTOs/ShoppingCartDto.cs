using System;

namespace BusinessLogic.DTOs;

public class ShoppingCartDto
{
    public List<CartItemDto> CartItems { get; set; } = [];
    public int ItemCount { get; set; }
    // public decimal SubTotal { get; set; } 
    public decimal Total { get; set; }
}

using System;

namespace BusinessLogic.DTOs;

public class ShoppingCartDto
{
    public List<CartItemDto> CartItems { get; set; } = [];
    public decimal Total { get; set; }
}

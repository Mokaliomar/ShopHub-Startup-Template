using System;

namespace BusinessLogic.DTOs;

public class CartItemDto
{
    /* public int ProductId { get; set;}
    public string ImageUrl { get; set; } = "";
    public string ProductName { get; set;} = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; } */
    public int Id { get; set; }
    public string Img { get; set; } = "";
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

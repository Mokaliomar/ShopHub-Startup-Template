using System;

namespace myshop.Web.ViewModels;

public class CartItemVM
{
    public int Id { get; set; }
    public string Img { get; set; } = "";
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; }

}

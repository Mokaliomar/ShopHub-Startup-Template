using System;

namespace myshop.Web.ViewModels;

public class ProductShopIndexVM
{
    public int Id { get; set; }
    public string? Image { get; set; }
    public string CategoryName { get; set; } = "";
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
}

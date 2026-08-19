using System;
using DataAccess.Models;
using Stripe.Terminal;

namespace myshop.Web.ViewModels;

public class ShoppingCartVM
{
    public List<CartItemVM> CartItems { get; set; } = [];
    public int ItemCount { get; set; }
    // public decimal SubTotal { get; set; } 
    public decimal Total { get; set; }
}

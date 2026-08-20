using System;
using DataAccess.Models;

namespace myshop.Web.ViewModels;

public class MyOrdersVM
{
    public int OrderID { get; set; }
    // public string OrderName { get; set; } = "";
    public DateTime OrderDate { get; set; }
    public DateTime? ShippingDate { get; set; }

    public decimal TotalPrice { get; set; }

    public string? OrderStatus { get; set; }
    public string? PaymentStatus { get; set; }

    public IEnumerable<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}

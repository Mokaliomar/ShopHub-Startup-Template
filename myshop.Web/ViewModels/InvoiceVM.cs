using System;
using DataAccess.Models;

namespace myshop.Web.ViewModels;

public class InvoiceVM
{
    public OrderHeader OrderHeader { get; set; } = new OrderHeader();
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}

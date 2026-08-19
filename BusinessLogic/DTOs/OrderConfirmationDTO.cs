using System;

namespace BusinessLogic.DTOs;

public class OrderConfirmationDTO
{
    public string CustomerName { get; set; } = "";
    public int OrderId { get; set;}
    public IEnumerable<CartItemDto> OrderItems { get; set; } = new List<CartItemDto>();
    public decimal TotalPrice { get; set; }
    public string ShippingAddress { get; set; } = "";
    public string City { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string EstimatedDeliveryDate { get; set; } = DateTime.Now.AddDays(2).ToString("dd MMM yyyy");
}

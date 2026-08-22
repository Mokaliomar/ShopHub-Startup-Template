using System;
using DataAccess.Models;

namespace BusinessLogic.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string? Image { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public float AverageRate { get; set; }
    public int ReviewsCount { get; set; }
    // public IEnumerable<Review> Reviews { get; set; } = new List<Review>();
}

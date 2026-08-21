using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using myshop.Web.Interfaces;

namespace DataAccess.Models;

public class Review : IAuditable
{
    public int Id { get; set;}
    public string TheReview { get; set; } = "";
    public float ProductRate { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string ApplicationUserId { get; set; }
    [ValidateNever]
    public ApplicationUser ApplicationUser { get; set; }

    public int ProductId { get; set; }
    [ValidateNever]
    public Product Product { get; set; }
}

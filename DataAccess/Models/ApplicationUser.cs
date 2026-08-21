using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using myshop.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ApplicationUser : IdentityUser, IAuditable
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }

        [ValidateNever]
        public ICollection<OrderHeader> OrderHeaders { get; set; } = new List<OrderHeader>();
    
        public ICollection<Review> UserReviews { get; set; } = new List<Review>();
    }
}

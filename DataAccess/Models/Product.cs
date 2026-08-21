using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using myshop.Web.Interfaces;

namespace DataAccess.Models
{
    public class Product : IAuditable
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        /* [DisplayName("Image")]*/
        [ValidateNever]
        public string Img { get; set; }

        public decimal Price { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }


        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }

        #region Relationships

        // [DisplayName("Category")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        
        public ICollection<Review> ProductReviews { get; set; } = new List<Review>();
        #endregion
    }
}

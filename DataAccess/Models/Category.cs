using System.ComponentModel.DataAnnotations;
using DataAccess.Migrations;

namespace DataAccess.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }

        public IEnumerable<Product> Products { get; set; } = new List<Product>();

    }
}

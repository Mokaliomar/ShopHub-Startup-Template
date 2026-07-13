using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace DataAccess.Data;

public static class SeedData
{
    public static void SeedDataModel(this ModelBuilder modelBuilder)
    {
        // 1. ضخ الأقسام (Categories) بأرقام IDs ثابته
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Laptops",
                Description = "Modern laptops for work and gaming.",
                CreatedTime = new DateTime(2026, 1, 19)
            },
            new Category
            {
                Id = 2,
                Name = "Smartphones",
                Description = "Latest smartphones with advanced features.",
                CreatedTime = new DateTime(2026, 3, 31)
            },
            new Category
            {
                Id = 3,
                Name = "Accessories",
                Description = "Useful accessories for your devices.",
                CreatedTime = new DateTime(2026, 5, 17)
            }
        );

        // 2. ضخ المنتجات (Products) وربط كل منتج بالـ CategoryId الصح
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "MacBook Pro 14",
                Description = "Apple M3 chip, 16GB RAM, 512GB SSD, Space Gray.",
                Price = 1999.00m,
                Img = "Images/Products/4eb463ee-d056-4ff7-94a0-bc2418c1f866.png",
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Name = "Dell XPS 13",
                Description = "Intel Core i7, 16GB RAM, 512GB SSD, InfinityEdge Display.",
                Price = 1249.00m,
                Img = "Images/Products/ef491e09-7bef-4e18-af5c-71441104b8eb.jpg",
                CategoryId = 1
            },
            new Product
            {
                Id = 3,
                Name = "iPhone 15 Pro",
                Description = "Titanium design, A17 Pro chip, 48MP Main camera.",
                Price = 999.00m,
                Img = "Images/Products/c42a18f3-79d6-44c9-9047-438d295dbacd.webp",
                CategoryId = 2
            },
            new Product
            {
                Id = 4,
                Name = "Samsung Galaxy A17",
                Description = "Dynamic AMOLED 2X, AI Camera Features, 128GB Storage.",
                Price = 799.00m,
                Img = "Images/Products/c1db582a-79c3-40ca-bdf6-f939bdd40e5a.jpg",
                CategoryId = 2
            },
            new Product
            {
                Id = 5,
                Name = "Samsung S20 FE 5G",
                Description = "Dynamic AMOLED 2X, AI Camera Features, 256GB Storage.",
                Price = 249.00m,
                Img = "Images/Products/7462ab44-1ae1-490d-bf55-c3c550ce72c2.jpg",
                CategoryId = 3
            }
        );
    }
}

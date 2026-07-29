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
            },
            
            //* Adding New Products
            new Product
            {
                Id = 6,
                Name = "Lenovo Legion Pro 5",
                Description = "AMD Ryzen 7, RTX 4060, 16GB RAM, 1TB SSD, 165Hz Display.",
                Price = 1399.99m,
                Img = "Images/Products/6e4d4b55-22b1-4358-834f-2cecc6123b3c.jpg",
                CategoryId = 1
            },
            new Product
            {
                Id = 7,
                Name = "HP Spectre x360",
                Description = "Intel Core Ultra 7, 16GB RAM, 1TB SSD, Touch OLED Screen.",
                Price = 1549.50m,
                Img = "Images/Products/87789d13-f31e-4bbd-9343-a5ad90396fad.webp",
                CategoryId = 1
            },
            new Product
            {
                Id = 8,
                Name = "ASUS ROG Zephyrus G14",
                Description = "AMD Ryzen 9, RTX 4070, 32GB RAM, 1TB NVMe SSD.",
                Price = 1899.00m,
                Img = "Images/Products/ac682fc5-fad4-485e-a800-a2a2893fa8ab.jpg",
                CategoryId = 1
            },
            new Product
            {
                Id = 9,
                Name = "Acer Swift Go 14",
                Description = "Intel Core i5, 8GB RAM, 512GB SSD, Ultra Lightweight.",
                Price = 649.99m,
                Img = "Images/Products/default-laptop.png",
                CategoryId = 1
            },
            new Product
            {
                Id = 10,
                Name = "Google Pixel 8 Pro",
                Description = "Google Tensor G3, 12GB RAM, 128GB Storage, Obsidian Black.",
                Price = 899.00m,
                Img = "Images/Products/default-phone.png",
                CategoryId = 2
            },
            new Product
            {
                Id = 11,
                Name = "Xiaomi 13T Pro",
                Description = "MediaTek Dimensity 9200+, Leica Camera System, 512GB Storage.",
                Price = 699.00m,
                Img = "Images/Products/default-phone.png",
                CategoryId = 2
            },
            new Product
            {
                Id = 12,
                Name = "OnePlus 12",
                Description = "Snapdragon 8 Gen 3, 16GB RAM, 512GB, 100W Fast Charging.",
                Price = 799.99m,
                Img = "Images/Products/default-phone.png",
                CategoryId = 2
            },
            new Product
            {
                Id = 13,
                Name = "Nothing Phone (2)",
                Description = "Glyph Interface, Snapdragon 8+ Gen 1, 12GB RAM, Transparent Design.",
                Price = 599.00m,
                Img = "Images/Products/default-phone.png",
                CategoryId = 2
            },
            new Product
            {
                Id = 14,
                Name = "Logitech MX Master 3S",
                Description = "Performance Wireless Mouse, 8K DPI, Quiet Clicks, Ergonomic.",
                Price = 99.99m,
                Img = "Images/Products/default-accessory.png",
                CategoryId = 3
            },
            new Product
            {
                Id = 15,
                Name = "Keychron K2 Wireless Keyboard",
                Description = "Mechanical Gaming Keyboard, Gateron Red Switches, RGB Backlit.",
                Price = 89.00m,
                Img = "Images/Products/default-accessory.png",
                CategoryId = 3
            },
            new Product
            {
                Id = 16,
                Name = "Anker 737 Power Bank",
                Description = "24,000mAh 3-Port Laptop Portable Charger with 140W Output.",
                Price = 129.99m,
                Img = "Images/Products/default-accessory.png",
                CategoryId = 3
            },
            new Product
            {
                Id = 17,
                Name = "Sony WH-1000XM5",
                Description = "Wireless Industry Leading Noise Canceling Headphones, Silver.",
                Price = 348.00m,
                Img = "Images/Products/default-accessory.png",
                CategoryId = 3
            },
            new Product
            {
                Id = 18,
                Name = "Apple AirPods Pro (2nd Gen)",
                Description = "USB-C Charging, Active Noise Cancellation, Adaptive Audio.",
                Price = 239.00m,
                Img = "Images/Products/default-accessory.png",
                CategoryId = 3
            },
            new Product
            {
                Id = 19,
                Name = "Samsung T7 Shield 1TB SSD",
                Description = "Portable External Solid State Drive, USB 3.2 Gen 2, IP65 Water Resistant.",
                Price = 109.99m,
                Img = "Images/Products/default-accessory.png",
                CategoryId = 3
            },
            new Product
            {
                Id = 20,
                Name = "Dell UltraSharp 27 4K Monitor",
                Description = "IPS Black Panel, USB-C Hub, HDR400, Ergonomic Stand.",
                Price = 529.00m,
                Img = "Images/Products/default-accessory.png",
                CategoryId = 3
            }
        );
    }
}

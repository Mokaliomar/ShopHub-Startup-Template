using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedTime", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Modern laptops for work and gaming.", "Laptops" },
                    { 2, new DateTime(2026, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Latest smartphones with advanced features.", "Smartphones" },
                    { 3, new DateTime(2026, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Useful accessories for your devices.", "Accessories" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Img", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Apple M3 chip, 16GB RAM, 512GB SSD, Space Gray.", "https://images.unsplash.com/photo-1517336714731-489689fd1ca8?w=500", "MacBook Pro 14", 1999.00m },
                    { 2, 1, "Intel Core i7, 16GB RAM, 512GB SSD, InfinityEdge Display.", "https://images.unsplash.com/photo-1593642632823-8f785ba67e45?w=500", "Dell XPS 13", 1249.00m },
                    { 3, 2, "Titanium design, A17 Pro chip, 48MP Main camera.", "https://images.unsplash.com/photo-1510557880182-3d4d3cba35a5?w=500", "iPhone 15 Pro", 999.00m },
                    { 4, 2, "Dynamic AMOLED 2X, AI Camera Features, 128GB Storage.", "https://images.unsplash.com/photo-1610945265064-0e34e5519bbf?w=500", "Samsung Galaxy S24", 799.00m },
                    { 5, 3, "Active Noise Cancelling, 30-hour battery life, Bluetooth 5.2.", "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=500", "Wireless Headphones", 249.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}

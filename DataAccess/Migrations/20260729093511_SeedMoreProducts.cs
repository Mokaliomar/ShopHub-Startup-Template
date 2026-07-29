using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Img", "Name", "Price" },
                values: new object[,]
                {
                    { 6, 1, "AMD Ryzen 7, RTX 4060, 16GB RAM, 1TB SSD, 165Hz Display.", "Images/Products/default-laptop.png", "Lenovo Legion Pro 5", 1399.99m },
                    { 7, 1, "Intel Core Ultra 7, 16GB RAM, 1TB SSD, Touch OLED Screen.", "Images/Products/default-laptop.png", "HP Spectre x360", 1549.50m },
                    { 8, 1, "AMD Ryzen 9, RTX 4070, 32GB RAM, 1TB NVMe SSD.", "Images/Products/default-laptop.png", "ASUS ROG Zephyrus G14", 1899.00m },
                    { 9, 1, "Intel Core i5, 8GB RAM, 512GB SSD, Ultra Lightweight.", "Images/Products/default-laptop.png", "Acer Swift Go 14", 649.99m },
                    { 10, 2, "Google Tensor G3, 12GB RAM, 128GB Storage, Obsidian Black.", "Images/Products/default-phone.png", "Google Pixel 8 Pro", 899.00m },
                    { 11, 2, "MediaTek Dimensity 9200+, Leica Camera System, 512GB Storage.", "Images/Products/default-phone.png", "Xiaomi 13T Pro", 699.00m },
                    { 12, 2, "Snapdragon 8 Gen 3, 16GB RAM, 512GB, 100W Fast Charging.", "Images/Products/default-phone.png", "OnePlus 12", 799.99m },
                    { 13, 2, "Glyph Interface, Snapdragon 8+ Gen 1, 12GB RAM, Transparent Design.", "Images/Products/default-phone.png", "Nothing Phone (2)", 599.00m },
                    { 14, 3, "Performance Wireless Mouse, 8K DPI, Quiet Clicks, Ergonomic.", "Images/Products/default-accessory.png", "Logitech MX Master 3S", 99.99m },
                    { 15, 3, "Mechanical Gaming Keyboard, Gateron Red Switches, RGB Backlit.", "Images/Products/default-accessory.png", "Keychron K2 Wireless Keyboard", 89.00m },
                    { 16, 3, "24,000mAh 3-Port Laptop Portable Charger with 140W Output.", "Images/Products/default-accessory.png", "Anker 737 Power Bank", 129.99m },
                    { 17, 3, "Wireless Industry Leading Noise Canceling Headphones, Silver.", "Images/Products/default-accessory.png", "Sony WH-1000XM5", 348.00m },
                    { 18, 3, "USB-C Charging, Active Noise Cancellation, Adaptive Audio.", "Images/Products/default-accessory.png", "Apple AirPods Pro (2nd Gen)", 239.00m },
                    { 19, 3, "Portable External Solid State Drive, USB 3.2 Gen 2, IP65 Water Resistant.", "Images/Products/default-accessory.png", "Samsung T7 Shield 1TB SSD", 109.99m },
                    { 20, 3, "IPS Black Panel, USB-C Hub, HDR400, Ergonomic Stand.", "Images/Products/default-accessory.png", "Dell UltraSharp 27 4K Monitor", 529.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}

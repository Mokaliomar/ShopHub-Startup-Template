using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Img",
                value: "/Images/Products/ef491e09-7bef-4e18-af5c-71441104b8eb.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Img",
                value: "/Images/Products/c42a18f3-79d6-44c9-9047-438d295dbacd.webp");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Img", "Name" },
                values: new object[] { "/Images/Products/c1db582a-79c3-40ca-bdf6-f939bdd40e5a.jpg", "Samsung Galaxy A17" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Img", "Name" },
                values: new object[] { "Dynamic AMOLED 2X, AI Camera Features, 256GB Storage.", "/Images/Products/7462ab44-1ae1-490d-bf55-c3c550ce72c2.jpg", "Samsung S20 FE 5G" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Img",
                value: "https://images.unsplash.com/photo-1593642632823-8f785ba67e45?w=500");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Img",
                value: "https://images.unsplash.com/photo-1510557880182-3d4d3cba35a5?w=500");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Img", "Name" },
                values: new object[] { "https://images.unsplash.com/photo-1610945265064-0e34e5519bbf?w=500", "Samsung Galaxy S24" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Img", "Name" },
                values: new object[] { "Active Noise Cancelling, 30-hour battery life, Bluetooth 5.2.", "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=500", "Wireless Headphones" });
        }
    }
}

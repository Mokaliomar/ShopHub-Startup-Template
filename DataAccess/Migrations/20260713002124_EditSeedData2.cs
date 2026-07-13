using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditSeedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Img",
                value: "Images/Products/4eb463ee-d056-4ff7-94a0-bc2418c1f866.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Img",
                value: "Images/Products/ef491e09-7bef-4e18-af5c-71441104b8eb.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Img",
                value: "Images/Products/c42a18f3-79d6-44c9-9047-438d295dbacd.webp");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "Img",
                value: "Images/Products/c1db582a-79c3-40ca-bdf6-f939bdd40e5a.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "Img",
                value: "Images/Products/7462ab44-1ae1-490d-bf55-c3c550ce72c2.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Img",
                value: "https://images.unsplash.com/photo-1517336714731-489689fd1ca8?w=500");

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
                column: "Img",
                value: "/Images/Products/c1db582a-79c3-40ca-bdf6-f939bdd40e5a.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "Img",
                value: "/Images/Products/7462ab44-1ae1-490d-bf55-c3c550ce72c2.jpg");
        }
    }
}

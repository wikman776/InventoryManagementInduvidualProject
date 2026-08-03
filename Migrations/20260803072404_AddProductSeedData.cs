using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventoryManagementInduvidualProject.Migrations
{
    /// <inheritdoc />
    public partial class AddProductSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "ProductCode", "Quantity" },
                values: new object[,]
                {
                    { 1, "Laptop", 899.99m, "P0001", 8 },
                    { 2, "Wireless Mouse", 29.99m, "P0002", 25 },
                    { 3, "Mechanical Keyboard", 79.99m, "P0003", 15 },
                    { 4, "Monitor", 249.99m, "P0004", 12 },
                    { 5, "USB-C Cable", 14.99m, "P0005", 50 },
                    { 6, "Office Chair", 199.99m, "P0006", 6 },
                    { 7, "Webcam", 59.99m, "P0007", 18 },
                    { 8, "External SSD", 119.99m, "P0008", 10 },
                    { 9, "Headset", 69.99m, "P0009", 20 },
                    { 10, "Desk Lamp", 39.99m, "P0010", 14 }
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
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class entitiyUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Products_ProductId1",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_ProductId1",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Stocks");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "Stocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId1",
                table: "Stocks",
                column: "ProductId1",
                unique: true,
                filter: "[ProductId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Products_ProductId1",
                table: "Stocks",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}

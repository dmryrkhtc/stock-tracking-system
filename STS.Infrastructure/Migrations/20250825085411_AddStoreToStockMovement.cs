using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStoreToStockMovement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Store",
                table: "StockMovements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Store",
                table: "StockMovements");
        }
    }
}

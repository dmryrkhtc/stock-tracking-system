using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixingEntityy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Stocks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Stocks");
        }
    }
}

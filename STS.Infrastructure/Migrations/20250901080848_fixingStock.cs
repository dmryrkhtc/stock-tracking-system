using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixingStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Unit",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_CompanyId",
                table: "Stocks",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Companies_CompanyId",
                table: "Stocks",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Companies_CompanyId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_CompanyId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Stocks");
        }
    }
}

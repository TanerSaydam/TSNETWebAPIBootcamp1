using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Middle.MinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class mg3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price_Currency",
                table: "Products",
                newName: "Currency");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "Products",
                newName: "Price_Currency");
        }
    }
}

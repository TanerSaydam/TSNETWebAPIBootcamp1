using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Middle.MinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class mg4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyCategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_MyCategoryId",
                table: "Products",
                column: "MyCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_MyCategoryId",
                table: "Products",
                column: "MyCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_MyCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_MyCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MyCategoryId",
                table: "Products");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Middle.MinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class mg2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "Products",
                newName: "Price_Currency");

            migrationBuilder.AlterColumn<int>(
                name: "Price_Currency",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.RenameColumn(
                name: "Price_Currency",
                table: "Products",
                newName: "Currency");

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "Products",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mataeem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "SellingPrice");

            migrationBuilder.AddColumn<int>(
                name: "MandatoryCount",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RegularPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MandatoryCount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "RegularPrice",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SellingPrice",
                table: "Products",
                newName: "Price");
        }
    }
}

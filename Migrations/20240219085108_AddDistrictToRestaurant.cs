using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mataeem.Migrations
{
    /// <inheritdoc />
    public partial class AddDistrictToRestaurant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "BusinessHours",
                newName: "OpenTime");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "BusinessHours",
                newName: "CloseTime");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "BusinessHours",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "BusinessHours");

            migrationBuilder.RenameColumn(
                name: "OpenTime",
                table: "BusinessHours",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "CloseTime",
                table: "BusinessHours",
                newName: "EndTime");
        }
    }
}

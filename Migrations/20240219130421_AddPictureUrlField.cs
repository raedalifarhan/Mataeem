using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mataeem.Migrations
{
    /// <inheritdoc />
    public partial class AddPictureUrlField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Restaurants");
        }
    }
}

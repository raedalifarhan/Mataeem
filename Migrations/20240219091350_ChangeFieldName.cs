using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mataeem.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFieldName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Day",
                table: "BusinessHours",
                newName: "DayOfWeek");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DayOfWeek",
                table: "BusinessHours",
                newName: "Day");
        }
    }
}

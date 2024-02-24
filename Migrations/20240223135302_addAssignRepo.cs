using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mataeem.Migrations
{
    /// <inheritdoc />
    public partial class addAssignRepo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AspNetUsers_CreatedById",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_DriverId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_CreatedById",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_DriverId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_DriverId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DriverId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Restaurants");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Restaurants",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "DriverId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssignedDriverId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "DriverRestaurants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DriverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RestaurantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAssigned = table.Column<bool>(type: "bit", nullable: false),
                    AssignDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverRestaurants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverRestaurants_AspNetUsers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverRestaurants_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AssignedDriverId",
                table: "Orders",
                column: "AssignedDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverRestaurants_DriverId",
                table: "DriverRestaurants",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverRestaurants_RestaurantId",
                table: "DriverRestaurants",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AspNetUsers_CreatedById",
                table: "Invoices",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_AssignedDriverId",
                table: "Orders",
                column: "AssignedDriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_CreatedById",
                table: "Restaurants",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AspNetUsers_CreatedById",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_AssignedDriverId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_CreatedById",
                table: "Restaurants");

            migrationBuilder.DropTable(
                name: "DriverRestaurants");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AssignedDriverId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AssignedDriverId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Restaurants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverId",
                table: "Restaurants",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DriverId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_DriverId",
                table: "Restaurants",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DriverId",
                table: "Orders",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AspNetUsers_CreatedById",
                table: "Invoices",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_DriverId",
                table: "Orders",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_CreatedById",
                table: "Restaurants",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_DriverId",
                table: "Restaurants",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

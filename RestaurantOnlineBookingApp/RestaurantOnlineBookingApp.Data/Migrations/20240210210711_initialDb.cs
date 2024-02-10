using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class initialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_GuestId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_GuestId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "Restaurants");

            migrationBuilder.CreateTable(
                name: "AppUserRestaurant",
                columns: table => new
                {
                    BookedRestaurantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GuestsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRestaurant", x => new { x.BookedRestaurantsId, x.GuestsId });
                    table.ForeignKey(
                        name: "FK_AppUserRestaurant_AspNetUsers_GuestsId",
                        column: x => x.GuestsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserRestaurant_Restaurants_BookedRestaurantsId",
                        column: x => x.BookedRestaurantsId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRestaurant_GuestsId",
                table: "AppUserRestaurant",
                column: "GuestsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserRestaurant");

            migrationBuilder.AddColumn<Guid>(
                name: "GuestId",
                table: "Restaurants",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_GuestId",
                table: "Restaurants",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_GuestId",
                table: "Restaurants",
                column: "GuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

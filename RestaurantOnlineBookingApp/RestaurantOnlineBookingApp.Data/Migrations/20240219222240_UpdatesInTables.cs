using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class UpdatesInTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserRestaurant");

            migrationBuilder.DropTable(
                name: "Table");

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("4391f0b0-f6f6-4f91-b383-320a2b6e78df"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("76f9f126-0d19-4723-b398-b9cdbf974afb"));

            migrationBuilder.AddColumn<Guid>(
                name: "GuestId",
                table: "Restaurants",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "GuestId", "ImageUrl", "Name", "OwnerId", "Rating" },
                values: new object[] { new Guid("28344afb-1c44-4dc5-84a8-c7d9b7e16440"), "Ivan Vazov 26", 100, 2, 1, "Best food from Asia", null, "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg", "Asian Buffet", new Guid("92bc2551-2565-4fc4-8ed6-98deffe533b9"), 4.0 });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "GuestId", "ImageUrl", "Name", "OwnerId", "Rating" },
                values: new object[] { new Guid("df7cb379-10ef-4bdc-8574-af68731718b6"), "Hristo Botev 76", 50, 6, 1, "Best chinese in the country", null, "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg", "Best Of China", new Guid("92bc2551-2565-4fc4-8ed6-98deffe533b9"), 5.0 });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_GuestId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_GuestId",
                table: "Restaurants");

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("28344afb-1c44-4dc5-84a8-c7d9b7e16440"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("df7cb379-10ef-4bdc-8574-af68731718b6"));

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

            migrationBuilder.CreateTable(
                name: "Table",
                columns: table => new
                {
                    TableNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table", x => x.TableNumber);
                    table.ForeignKey(
                        name: "FK_Table_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "ImageUrl", "Name", "OwnerId", "Rating" },
                values: new object[] { new Guid("4391f0b0-f6f6-4f91-b383-320a2b6e78df"), "Hristo Botev 76", 50, 6, 1, "Best chinese in the country", "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg", "Best Of China", new Guid("92bc2551-2565-4fc4-8ed6-98deffe533b9"), 5.0 });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "ImageUrl", "Name", "OwnerId", "Rating" },
                values: new object[] { new Guid("76f9f126-0d19-4723-b398-b9cdbf974afb"), "Ivan Vazov 26", 100, 2, 1, "Best food from Asia", "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg", "Asian Buffet", new Guid("92bc2551-2565-4fc4-8ed6-98deffe533b9"), 4.0 });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRestaurant_GuestsId",
                table: "AppUserRestaurant",
                column: "GuestsId");

            migrationBuilder.CreateIndex(
                name: "IX_Table_RestaurantId",
                table: "Table",
                column: "RestaurantId");
        }
    }
}

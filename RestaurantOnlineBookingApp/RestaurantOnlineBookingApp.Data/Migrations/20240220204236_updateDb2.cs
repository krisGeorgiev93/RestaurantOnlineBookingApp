using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class updateDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("28344afb-1c44-4dc5-84a8-c7d9b7e16440"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("df7cb379-10ef-4bdc-8574-af68731718b6"));

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Restaurants");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndingTime",
                table: "Restaurants",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartingTime",
                table: "Restaurants",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "EndingTime", "GuestId", "ImageUrl", "Name", "OwnerId", "StartingTime" },
                values: new object[] { new Guid("46289cac-faaa-4c7c-bb5d-3cbcd841e244"), "Ivan Vazov 26", 100, 2, 1, "Best food from Asia", new TimeSpan(0, 0, 0, 0, 0), null, "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg", "Asian Buffet", new Guid("92bc2551-2565-4fc4-8ed6-98deffe533b9"), new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "EndingTime", "GuestId", "ImageUrl", "Name", "OwnerId", "StartingTime" },
                values: new object[] { new Guid("b3f4fe5d-b0e0-44ab-961e-93c1c6a4549e"), "Hristo Botev 76", 50, 6, 1, "Best chinese in the country", new TimeSpan(0, 0, 0, 0, 0), null, "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg", "Best Of China", new Guid("92bc2551-2565-4fc4-8ed6-98deffe533b9"), new TimeSpan(0, 0, 0, 0, 0) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("46289cac-faaa-4c7c-bb5d-3cbcd841e244"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("b3f4fe5d-b0e0-44ab-961e-93c1c6a4549e"));

            migrationBuilder.DropColumn(
                name: "EndingTime",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "StartingTime",
                table: "Restaurants");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Restaurants",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "GuestId", "ImageUrl", "Name", "OwnerId", "Rating" },
                values: new object[] { new Guid("28344afb-1c44-4dc5-84a8-c7d9b7e16440"), "Ivan Vazov 26", 100, 2, 1, "Best food from Asia", null, "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg", "Asian Buffet", new Guid("92bc2551-2565-4fc4-8ed6-98deffe533b9"), 4.0 });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "GuestId", "ImageUrl", "Name", "OwnerId", "Rating" },
                values: new object[] { new Guid("df7cb379-10ef-4bdc-8574-af68731718b6"), "Hristo Botev 76", 50, 6, 1, "Best chinese in the country", null, "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg", "Best Of China", new Guid("92bc2551-2565-4fc4-8ed6-98deffe533b9"), 5.0 });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class updateRestaurantEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("0d2baa67-dce0-4a77-a1f8-614f7cbbfecc"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("cd2e7147-2adb-4c5a-9563-c2d9077dfa7c"));

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Restaurants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "ImageUrl", "Name", "OwnerId", "Rating" },
                values: new object[] { new Guid("4391f0b0-f6f6-4f91-b383-320a2b6e78df"), "Hristo Botev 76", 50, 6, 1, "Best chinese in the country", "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg", "Best Of China", new Guid("92bc2551-2565-4fc4-8ed6-98deffe533b9"), 5.0 });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "ImageUrl", "Name", "OwnerId", "Rating" },
                values: new object[] { new Guid("76f9f126-0d19-4723-b398-b9cdbf974afb"), "Ivan Vazov 26", 100, 2, 1, "Best food from Asia", "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg", "Asian Buffet", new Guid("92bc2551-2565-4fc4-8ed6-98deffe533b9"), 4.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("4391f0b0-f6f6-4f91-b383-320a2b6e78df"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("76f9f126-0d19-4723-b398-b9cdbf974afb"));

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Restaurants");

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "CategoryId", "CityId", "Description", "ImageUrl", "Name", "OwnerId", "Rating" },
                values: new object[] { new Guid("0d2baa67-dce0-4a77-a1f8-614f7cbbfecc"), "Hristo Botev 76", 6, 1, "Best chinese in the country", "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg", "Best Of China", new Guid("92bc2551-2565-4fc4-8ed6-98deffe533b9"), 5.0 });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "CategoryId", "CityId", "Description", "ImageUrl", "Name", "OwnerId", "Rating" },
                values: new object[] { new Guid("cd2e7147-2adb-4c5a-9563-c2d9077dfa7c"), "Ivan Vazov 26", 2, 1, "Best food from Asia", "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg", "Asian Buffet", new Guid("92bc2551-2565-4fc4-8ed6-98deffe533b9"), 4.0 });
        }
    }
}

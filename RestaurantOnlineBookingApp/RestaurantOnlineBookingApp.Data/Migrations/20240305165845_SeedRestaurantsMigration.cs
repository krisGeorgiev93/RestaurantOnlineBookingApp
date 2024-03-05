using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class SeedRestaurantsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("46dce463-b9b6-4e67-8433-2e498a480e8e"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("58f20530-62c9-4846-a29b-b31b37ca1316"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("7d57d200-7810-4a2a-8fa1-8b017e0f33a6"));

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "EndingTime", "GuestId", "ImageUrl","IsActive", "Name", "OwnerId", "StartingTime" },
                values: new object[,]
                {
                    { new Guid("0ffc6e4c-98a6-427b-adc5-ddbaaee5050c"), "Ivan Ivanov 26", 100, 2, 1, "Best food from Asia", new TimeSpan(0, 23, 30, 0, 0), null, "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg", true, "Asian Buffet", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 17, 0, 0, 0) },
                    { new Guid("2c5ac627-3668-40c6-afad-061f529ad32c"), "Hristo Hristov 74", 150, 1, 2, "Traditional food from Bulgarian Kitchen", new TimeSpan(0, 23, 45, 0, 0), null, "https://media-cdn.tripadvisor.com/media/photo-s/03/b7/8b/ed/chevermeto-traditional.jpg", true,"Bulgarian Taste", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 12, 0, 0, 0) },
                    { new Guid("a20bcba7-b77d-463d-89cc-01e411008e84"), "Georgi Ivanov 26", 135, 4, 3, "Best food from Turkey", new TimeSpan(0, 23, 30, 0, 0), null, "https://images.squarespace-cdn.com/content/v1/62f4e2e25f95bb5d35522adc/0c8e42d8-aa26-4c9e-acbc-bcfd336b3731/Havin_a_Turkish_X_socialawakening+18.jpg", true, "Turkish Restaurant", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 12, 0, 0, 0) },
                    { new Guid("df9750d5-b25c-4046-bf29-2c563d0b56ff"), "Hristo Botev 76", 50, 6, 1, "Best chinese in the country", new TimeSpan(0, 23, 0, 0, 0), null, "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg", true, "Best Of China", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 18, 0, 0, 0) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("0ffc6e4c-98a6-427b-adc5-ddbaaee5050c"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("2c5ac627-3668-40c6-afad-061f529ad32c"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("a20bcba7-b77d-463d-89cc-01e411008e84"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("df9750d5-b25c-4046-bf29-2c563d0b56ff"));

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "EndingTime", "GuestId", "ImageUrl", "IsActive", "Name", "OwnerId", "StartingTime" },
                values: new object[] { new Guid("46dce463-b9b6-4e67-8433-2e498a480e8e"), "Ivan Ivanov 26", 100, 2, 1, "Best food from Asia", new TimeSpan(0, 23, 30, 0, 0), null, "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg", false, "Asian Buffet", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 17, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "EndingTime", "GuestId", "ImageUrl", "IsActive", "Name", "OwnerId", "StartingTime" },
                values: new object[] { new Guid("58f20530-62c9-4846-a29b-b31b37ca1316"), "Hristo Hristov 74", 150, 1, 2, "Traditional food from Bulgarian Kitchen", new TimeSpan(0, 23, 45, 0, 0), null, "https://media-cdn.tripadvisor.com/media/photo-s/03/b7/8b/ed/chevermeto-traditional.jpg", false, "Bulgarian Taste", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 12, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "EndingTime", "GuestId", "ImageUrl", "IsActive", "Name", "OwnerId", "StartingTime" },
                values: new object[] { new Guid("7d57d200-7810-4a2a-8fa1-8b017e0f33a6"), "Hristo Botev 76", 50, 6, 1, "Best chinese in the country", new TimeSpan(0, 23, 0, 0, 0), null, "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg", false, "Best Of China", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 18, 0, 0, 0) });
        }
    }
}

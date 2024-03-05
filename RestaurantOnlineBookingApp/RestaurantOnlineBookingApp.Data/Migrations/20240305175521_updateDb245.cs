using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class updateDb245 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("2b11f6db-5a55-462e-a32c-e239b2757c63"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("43e939b3-753d-4cb1-a6b9-144ec851dc91"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("9263ad29-840f-4c01-a54e-287a3a00ba5e"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("c0278afa-b978-47cc-a0d1-2e417ba3628c"));

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "EndingTime", "GuestId", "ImageUrl", "IsActive", "Name", "OwnerId", "StartingTime" },
                values: new object[,]
                {
                    { new Guid("1441b531-715c-4e78-b500-b0cc1355c183"), "Ivan Ivanov 26", 100, 2, 1, "Best food from Asia", new TimeSpan(0, 23, 30, 0, 0), null, "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg", true, "Asian Buffet", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 17, 0, 0, 0) },
                    { new Guid("22818fe6-6c34-4ce7-9c2a-25c6485cadce"), "Hristo Botev 76", 50, 6, 1, "Best chinese in the country", new TimeSpan(0, 23, 0, 0, 0), null, "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg", true, "Best Of China", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 18, 0, 0, 0) },
                    { new Guid("ba56ab36-68c2-4b30-b6f4-8ee816372e77"), "Georgi Ivanov 26", 135, 4, 3, "Best food from Turkey", new TimeSpan(0, 23, 30, 0, 0), null, "https://images.squarespace-cdn.com/content/v1/62f4e2e25f95bb5d35522adc/0c8e42d8-aa26-4c9e-acbc-bcfd336b3731/Havin_a_Turkish_X_socialawakening+18.jpg", true, "Turkish Restaurant", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 12, 0, 0, 0) },
                    { new Guid("fe3ce451-4f72-468c-a9b7-f0d1f998be3b"), "Hristo Hristov 74", 150, 1, 2, "Traditional food from Bulgarian Kitchen", new TimeSpan(0, 23, 45, 0, 0), null, "https://media-cdn.tripadvisor.com/media/photo-s/03/b7/8b/ed/chevermeto-traditional.jpg", true, "Bulgarian Taste", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 12, 0, 0, 0) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("1441b531-715c-4e78-b500-b0cc1355c183"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("22818fe6-6c34-4ce7-9c2a-25c6485cadce"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("ba56ab36-68c2-4b30-b6f4-8ee816372e77"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("fe3ce451-4f72-468c-a9b7-f0d1f998be3b"));

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "EndingTime", "GuestId", "ImageUrl", "IsActive", "Name", "OwnerId", "StartingTime" },
                values: new object[,]
                {
                    { new Guid("2b11f6db-5a55-462e-a32c-e239b2757c63"), "Hristo Hristov 74", 150, 1, 2, "Traditional food from Bulgarian Kitchen", new TimeSpan(0, 23, 45, 0, 0), null, "https://media-cdn.tripadvisor.com/media/photo-s/03/b7/8b/ed/chevermeto-traditional.jpg", false, "Bulgarian Taste", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 12, 0, 0, 0) },
                    { new Guid("43e939b3-753d-4cb1-a6b9-144ec851dc91"), "Hristo Botev 76", 50, 6, 1, "Best chinese in the country", new TimeSpan(0, 23, 0, 0, 0), null, "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg", false, "Best Of China", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 18, 0, 0, 0) },
                    { new Guid("9263ad29-840f-4c01-a54e-287a3a00ba5e"), "Georgi Ivanov 26", 135, 4, 3, "Best food from Turkey", new TimeSpan(0, 23, 30, 0, 0), null, "https://images.squarespace-cdn.com/content/v1/62f4e2e25f95bb5d35522adc/0c8e42d8-aa26-4c9e-acbc-bcfd336b3731/Havin_a_Turkish_X_socialawakening+18.jpg", false, "Turkish Restaurant", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 12, 0, 0, 0) },
                    { new Guid("c0278afa-b978-47cc-a0d1-2e417ba3628c"), "Ivan Ivanov 26", 100, 2, 1, "Best food from Asia", new TimeSpan(0, 23, 30, 0, 0), null, "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg", false, "Asian Buffet", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 17, 0, 0, 0) }
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class updateSeedOfRestaurants1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Categories_CategoryId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Cities_CityId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Owners_OwnerId",
                table: "Restaurants");

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

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Restaurants",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Categories_CategoryId",
                table: "Restaurants",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Cities_CityId",
                table: "Restaurants",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Owners_OwnerId",
                table: "Restaurants",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.InsertData(
       table: "Restaurants",
       columns: new[] { "Id", "Name", "Address", "Description", "StartingTime", "EndingTime", "ImageUrl","IsActive", "Capacity", "CityId", "CategoryId", "OwnerId" },
       values: new object[,]
       {
            { new Guid("1441b531-715c-4e78-b500-b0cc1355c183"), "Turkish Restaurant", "Georgi Ivanov 26", "Best food from Turkey", new TimeSpan(12, 0, 0), new TimeSpan(23, 30, 0), "https://images.squarespace-cdn.com/content/v1/62f4e2e25f95bb5d35522adc/0c8e42d8-aa26-4c9e-acbc-bcfd336b3731/Havin_a_Turkish_X_socialawakening+18.jpg",true, 135, 3, 4, Guid.Parse("C4F8569C-1CDA-4B0B-94E4-16B44A4631CF") },
            { new Guid("22818fe6-6c34-4ce7-9c2a-25c6485cadce"), "Asian Buffet", "Ivan Ivanov 26", "Best food from Asia", new TimeSpan(17, 0, 0), new TimeSpan(23, 30, 0), "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg",true, 100, 1, 2, Guid.Parse("C4F8569C-1CDA-4B0B-94E4-16B44A4631CF") },
            { new Guid("ba56ab36-68c2-4b30-b6f4-8ee816372e77"), "Best Of China", "Hristo Botev 76", "Best chinese in the country", new TimeSpan(18, 0, 0), new TimeSpan(23, 0, 0), "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg",true, 50, 1, 6, Guid.Parse("C4F8569C-1CDA-4B0B-94E4-16B44A4631CF") },
            { new Guid("fe3ce451-4f72-468c-a9b7-f0d1f998be3b"), "Bulgarian Taste", "Hristo Hristov 74", "Traditional food from Bulgarian Kitchen", new TimeSpan(12, 0, 0), new TimeSpan(23, 45, 0), "https://media-cdn.tripadvisor.com/media/photo-s/03/b7/8b/ed/chevermeto-traditional.jpg",true, 150, 2, 1, Guid.Parse("C4F8569C-1CDA-4B0B-94E4-16B44A4631CF") }
       });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Categories_CategoryId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Cities_CityId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Owners_OwnerId",
                table: "Restaurants");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Restaurants",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Categories_CategoryId",
                table: "Restaurants",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Cities_CityId",
                table: "Restaurants",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Owners_OwnerId",
                table: "Restaurants",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class ChangeSeedDataClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("38361ab1-27ea-49dc-96a1-8642266be01e"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("b82eb850-4371-4615-8c14-b7930adc7dde"));

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 11, "The best salad in Greece", "https://www.themediterraneandish.com/wp-content/uploads/2023/08/Greek-salad-web-story-poster-image.jpeg", "Greek Salad", 2.5 },
                    { 22, "The best salad in Bulgaria", "https://www.craftbeering.com/wp-content/uploads/2020/07/Shopska-salad-Original-Bulgarian-cucumber-tomato-salad-1-720x720.jpg", "Shopska Salad", 2.6000000000000001 }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Try our delicious food", "Menu1" },
                    { 2, "Try our delicious food", "Menu2" }
                });

            migrationBuilder.InsertData(
                table: "MenuMeals",
                columns: new[] { "MealId", "MenuId" },
                values: new object[,]
                {
                    { 11, 1 },
                    { 22, 2 }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "EndingTime", "GuestId", "ImageUrl", "MenuId", "MenuId1", "Name", "OwnerId", "StartingTime" },
                values: new object[,]
                {
                    { new Guid("f6f5e385-8ae2-473b-88f4-1c36536cae5a"), "Ivan Ivanov 2", 100, 2, 1, "Best food from Asia", new TimeSpan(0, 23, 30, 0, 0), null, "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg", 1, null, "Asian Buffet", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 17, 0, 0, 0) },
                    { new Guid("ff89ec22-93a9-49dd-a6e8-c2862f9d77a7"), "Hristo Botev 7", 50, 6, 1, "Best chinese in the country", new TimeSpan(0, 23, 0, 0, 0), null, "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg", 2, null, "Best Of China", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 18, 0, 0, 0) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuMeals",
                keyColumns: new[] { "MealId", "MenuId" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                table: "MenuMeals",
                keyColumns: new[] { "MealId", "MenuId" },
                keyValues: new object[] { 22, 2 });

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("f6f5e385-8ae2-473b-88f4-1c36536cae5a"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("ff89ec22-93a9-49dd-a6e8-c2862f9d77a7"));

            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "EndingTime", "GuestId", "ImageUrl", "IsActive", "MenuId", "MenuId1", "Name", "OwnerId", "StartingTime" },
                values: new object[] { new Guid("38361ab1-27ea-49dc-96a1-8642266be01e"), "Ivan Ivanov 26", 100, 2, 1, "Best food from Asia", new TimeSpan(0, 23, 30, 0, 0), null, "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg", false, null, null, "Asian Buffet", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 17, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "EndingTime", "GuestId", "ImageUrl", "IsActive", "MenuId", "MenuId1", "Name", "OwnerId", "StartingTime" },
                values: new object[] { new Guid("b82eb850-4371-4615-8c14-b7930adc7dde"), "Hristo Botev 76", 50, 6, 1, "Best chinese in the country", new TimeSpan(0, 23, 0, 0, 0), null, "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg", false, null, null, "Best Of China", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 18, 0, 0, 0) });
        }
    }
}

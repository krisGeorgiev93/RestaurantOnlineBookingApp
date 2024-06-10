using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class UpdateDbSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CapacitiesParDate_Restaurants_RestaurantId",
                table: "CapacitiesParDate");

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

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "Name2",
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "Name1",
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("8099b56d-7710-415d-9c06-4569082c6758"), 0, "1e2698ba-910c-4afd-a1e0-db17656bcbff", "ivan@mail.com", false, "Ivan", "Georgiev", false, null, "ivan@mail.com", "ivan@mail.com", "AQAAAAEAACcQAAAAEKke0ih0OggX8odq8jq++ZHAekd61t4qHu7BZIs3HTs06Pc3EIvoNdI5lhYENA/TmQ==", null, false, null, false, "ivan@mail.com" },
                    { new Guid("faf6dc41-ce01-44a9-b63c-0abd2df2d15f"), 0, "380bd0dd-8bdd-426b-b056-2e8a4c249227", "kiril@mail.com", false, "Kiril", "Ivanov", false, null, "kiril@mail.com", "kiril@mail.com", "AQAAAAEAACcQAAAAEGPquHZMA7zfzgsu5fxR9Erw3QZOl0JEJKtLUlLsISAKxYofRnve8IMbbfYcpMLqIQ==", null, false, null, false, "kiril@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Family" },
                    { 2, "Buffet" },
                    { 3, "Coffee house" },
                    { 4, "Mediterranean" },
                    { 5, "Desert House" },
                    { 6, "Chinese" },
                    { 7, "Indian" },
                    { 8, "Lebanese" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CityName" },
                values: new object[,]
                {
                    { 1, "Sofia" },
                    { 2, "Plovdiv" },
                    { 3, "Varna" },
                    { 4, "Burgas" }
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[] { new Guid("44e7b2ef-dfa8-45ae-aca9-0b52b9a3df4d"), "+359888888888", new Guid("faf6dc41-ce01-44a9-b63c-0abd2df2d15f") });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "EndingTime", "GuestId", "ImageUrl", "IsActive", "Name", "OwnerId", "StartingTime" },
                values: new object[] { new Guid("3614f373-2355-4e6c-96e5-542f0689752f"), "Georgi Georgiev 66", 100, 1, 2, "Family restaurant with italian food", new TimeSpan(0, 23, 45, 0, 0), null, "https://www.japan-guide.com/g21/2036_family_01.jpg", true, "Restaurant Italy", new Guid("44e7b2ef-dfa8-45ae-aca9-0b52b9a3df4d"), new TimeSpan(0, 12, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "CapacitiesParDate",
                columns: new[] { "Id", "Capacity", "Date", "RestaurantId" },
                values: new object[,]
                {
                    { 1, 100, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 2, 100, new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 3, 100, new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 4, 100, new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 5, 100, new DateTime(2024, 6, 14, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 6, 100, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 7, 100, new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 8, 100, new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 9, 100, new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 10, 100, new DateTime(2024, 6, 19, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 11, 100, new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 12, 100, new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 13, 100, new DateTime(2024, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 14, 100, new DateTime(2024, 6, 23, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 15, 100, new DateTime(2024, 6, 24, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 16, 100, new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 17, 100, new DateTime(2024, 6, 26, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 18, 100, new DateTime(2024, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 19, 100, new DateTime(2024, 6, 28, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 20, 100, new DateTime(2024, 6, 29, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 21, 100, new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 22, 100, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 23, 100, new DateTime(2024, 7, 2, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 24, 100, new DateTime(2024, 7, 3, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 25, 100, new DateTime(2024, 7, 4, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 26, 100, new DateTime(2024, 7, 5, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 27, 100, new DateTime(2024, 7, 6, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 28, 100, new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 29, 100, new DateTime(2024, 7, 8, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 30, 100, new DateTime(2024, 7, 9, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 31, 100, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 32, 100, new DateTime(2024, 7, 11, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 33, 100, new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 34, 100, new DateTime(2024, 7, 13, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 35, 100, new DateTime(2024, 7, 14, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 36, 100, new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 37, 100, new DateTime(2024, 7, 16, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 38, 100, new DateTime(2024, 7, 17, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 39, 100, new DateTime(2024, 7, 18, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 40, 100, new DateTime(2024, 7, 19, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 41, 100, new DateTime(2024, 7, 20, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 42, 100, new DateTime(2024, 7, 21, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") }
                });

            migrationBuilder.InsertData(
                table: "CapacitiesParDate",
                columns: new[] { "Id", "Capacity", "Date", "RestaurantId" },
                values: new object[,]
                {
                    { 43, 100, new DateTime(2024, 7, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 44, 100, new DateTime(2024, 7, 23, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 45, 100, new DateTime(2024, 7, 24, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 46, 100, new DateTime(2024, 7, 25, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 47, 100, new DateTime(2024, 7, 26, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 48, 100, new DateTime(2024, 7, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 49, 100, new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 50, 100, new DateTime(2024, 7, 29, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 51, 100, new DateTime(2024, 7, 30, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 52, 100, new DateTime(2024, 7, 31, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 53, 100, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 54, 100, new DateTime(2024, 8, 2, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 55, 100, new DateTime(2024, 8, 3, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 56, 100, new DateTime(2024, 8, 4, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 57, 100, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 58, 100, new DateTime(2024, 8, 6, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 59, 100, new DateTime(2024, 8, 7, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 60, 100, new DateTime(2024, 8, 8, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "Description", "ImageUrl", "Price", "RestaurantId", "Time", "Title" },
                values: new object[] { new Guid("98ff6720-bf93-4fe0-931e-b8fbac9917d9"), new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Local), "Description for Special Event 1", "https://www.eventbookings.com/wp-content/uploads/2018/03/event-ideas-for-party-eventbookings.jpg", 50.00m, new Guid("3614f373-2355-4e6c-96e5-542f0689752f"), new TimeSpan(0, 18, 0, 0, 0), "Special Event 1" });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price", "RestaurantId" },
                values: new object[,]
                {
                    { 12, "Italian pizza with cheese and peperoni", "https://www.simplyrecipes.com/thmb/KE6iMblr3R2Db6oE8HdyVsFSj2A=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/__opt__aboutcom__coeus__resources__content_migration__simply_recipes__uploads__2019__09__easy-pepperoni-pizza-lead-3-1024x682-583b275444104ef189d693a64df625da.jpg", "Pizza Peperoni", 10.50m, new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 14, "Chinese chicken fried rice with eggs", "https://tildaricelive.s3.eu-central-1.amazonaws.com/wp-content/uploads/2021/05/04111234/chicken-fried-rice-low-res-2.png", "Chicken Fried Rice", 9.20m, new Guid("3614f373-2355-4e6c-96e5-542f0689752f") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CapacitiesParDate_Restaurants_RestaurantId",
                table: "CapacitiesParDate",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Categories_CategoryId",
                table: "Restaurants",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Cities_CityId",
                table: "Restaurants",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Owners_OwnerId",
                table: "Restaurants",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CapacitiesParDate_Restaurants_RestaurantId",
                table: "CapacitiesParDate");

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
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8099b56d-7710-415d-9c06-4569082c6758"));

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "CapacitiesParDate",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("98ff6720-bf93-4fe0-931e-b8fbac9917d9"));

            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("3614f373-2355-4e6c-96e5-542f0689752f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: new Guid("44e7b2ef-dfa8-45ae-aca9-0b52b9a3df4d"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("faf6dc41-ce01-44a9-b63c-0abd2df2d15f"));

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Restaurants",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16,
                oldDefaultValue: "Name2");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16,
                oldDefaultValue: "Name1");

            migrationBuilder.AddForeignKey(
                name: "FK_CapacitiesParDate_Restaurants_RestaurantId",
                table: "CapacitiesParDate",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}

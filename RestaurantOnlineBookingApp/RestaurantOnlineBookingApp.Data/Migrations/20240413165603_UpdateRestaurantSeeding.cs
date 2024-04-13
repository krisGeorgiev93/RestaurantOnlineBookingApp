using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class UpdateRestaurantSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("416a7e0d-3ba0-45b8-8613-8d00e239b3f0"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8099b56d-7710-415d-9c06-4569082c6758"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "342b2961-cdb5-48e6-ae8f-a9104841a425", "AQAAAAEAACcQAAAAEADJi9TDXOawx/EleRAmz3pduJoU763MyVsMKFYmCLGhvilHT//QmeDFIb87b4F3gQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("faf6dc41-ce01-44a9-b63c-0abd2df2d15f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "251fc4d2-ddab-4312-8e10-b6c130a49507", "AQAAAAEAACcQAAAAEF4SL90XdTmmxNDI0F3boYKJSl9XcAVqgschqGdw2UviDc0EB6fHvmnzhe5141e1ig==" });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "EndingTime", "GuestId", "ImageUrl", "IsActive", "Name", "OwnerId", "StartingTime" },
                values: new object[] { new Guid("3614f373-2355-4e6c-96e5-542f0689752f"), "Georgi Georgiev 66", 100, 1, 2, "Family restaurant with italian food", new TimeSpan(0, 23, 45, 0, 0), null, "https://www.japan-guide.com/g21/2036_family_01.jpg", true, "Restaurant Italy", new Guid("44e7b2ef-dfa8-45ae-aca9-0b52b9a3df4d"), new TimeSpan(0, 12, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "CapacitiesParDate",
                columns: new[] { "Id", "Capacity", "Date", "RestaurantId" },
                values: new object[,]
                {
                    { 1, 100, new DateTime(2024, 4, 13, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 2, 100, new DateTime(2024, 4, 14, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 3, 100, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 4, 100, new DateTime(2024, 4, 16, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 5, 100, new DateTime(2024, 4, 17, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 6, 100, new DateTime(2024, 4, 18, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 7, 100, new DateTime(2024, 4, 19, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 8, 100, new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 9, 100, new DateTime(2024, 4, 21, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 10, 100, new DateTime(2024, 4, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 11, 100, new DateTime(2024, 4, 23, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 12, 100, new DateTime(2024, 4, 24, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 13, 100, new DateTime(2024, 4, 25, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 14, 100, new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 15, 100, new DateTime(2024, 4, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 16, 100, new DateTime(2024, 4, 28, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 17, 100, new DateTime(2024, 4, 29, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 18, 100, new DateTime(2024, 4, 30, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 19, 100, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 20, 100, new DateTime(2024, 5, 2, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 21, 100, new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 22, 100, new DateTime(2024, 5, 4, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 23, 100, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 24, 100, new DateTime(2024, 5, 6, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 25, 100, new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 26, 100, new DateTime(2024, 5, 8, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 27, 100, new DateTime(2024, 5, 9, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 28, 100, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 29, 100, new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 30, 100, new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 31, 100, new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 32, 100, new DateTime(2024, 5, 14, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 33, 100, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 34, 100, new DateTime(2024, 5, 16, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 35, 100, new DateTime(2024, 5, 17, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 36, 100, new DateTime(2024, 5, 18, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 37, 100, new DateTime(2024, 5, 19, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 38, 100, new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 39, 100, new DateTime(2024, 5, 21, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 40, 100, new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 41, 100, new DateTime(2024, 5, 23, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 42, 100, new DateTime(2024, 5, 24, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") }
                });

            migrationBuilder.InsertData(
                table: "CapacitiesParDate",
                columns: new[] { "Id", "Capacity", "Date", "RestaurantId" },
                values: new object[,]
                {
                    { 43, 100, new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 44, 100, new DateTime(2024, 5, 26, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 45, 100, new DateTime(2024, 5, 27, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 46, 100, new DateTime(2024, 5, 28, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 47, 100, new DateTime(2024, 5, 29, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 48, 100, new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 49, 100, new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 50, 100, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 51, 100, new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 52, 100, new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 53, 100, new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 54, 100, new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 55, 100, new DateTime(2024, 6, 6, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 56, 100, new DateTime(2024, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 57, 100, new DateTime(2024, 6, 8, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 58, 100, new DateTime(2024, 6, 9, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 59, 100, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") },
                    { 60, 100, new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3614f373-2355-4e6c-96e5-542f0689752f") }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "Description", "ImageUrl", "Price", "RestaurantId", "Time", "Title" },
                values: new object[] { new Guid("773df392-6ed5-4d0c-9964-6b50b537c54f"), new DateTime(2024, 4, 18, 0, 0, 0, 0, DateTimeKind.Local), "Description for Special Event 1", "https://www.eventbookings.com/wp-content/uploads/2018/03/event-ideas-for-party-eventbookings.jpg", 50.00m, new Guid("3614f373-2355-4e6c-96e5-542f0689752f"), new TimeSpan(0, 18, 0, 0, 0), "Special Event 1" });

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 12,
                column: "RestaurantId",
                value: new Guid("3614f373-2355-4e6c-96e5-542f0689752f"));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 13,
                column: "RestaurantId",
                value: new Guid("3614f373-2355-4e6c-96e5-542f0689752f"));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 14,
                column: "RestaurantId",
                value: new Guid("3614f373-2355-4e6c-96e5-542f0689752f"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("773df392-6ed5-4d0c-9964-6b50b537c54f"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("3614f373-2355-4e6c-96e5-542f0689752f"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8099b56d-7710-415d-9c06-4569082c6758"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9802e766-4f70-472c-95e4-da7809cced17", "AQAAAAEAACcQAAAAEOpXj50sUluwVI5gtbIvZoi8pgUOLQsspa3yHu3pnSMet/h6yZsCAShg4ZJm40qTJw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("faf6dc41-ce01-44a9-b63c-0abd2df2d15f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d4633f71-a777-4be8-a896-48eb077f7231", "AQAAAAEAACcQAAAAEMWYmLAB9wqbrfy5gsmGxAkuezh3nggbx2KNH4eFISBF7iRg7wPuMb3WzeHL/LYRyQ==" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "Description", "ImageUrl", "Price", "RestaurantId", "Time", "Title" },
                values: new object[] { new Guid("416a7e0d-3ba0-45b8-8613-8d00e239b3f0"), new DateTime(2024, 4, 17, 0, 0, 0, 0, DateTimeKind.Local), "Description for Special Event 1", "https://www.eventbookings.com/wp-content/uploads/2018/03/event-ideas-for-party-eventbookings.jpg", 50.00m, new Guid("e681a1dd-b85f-4860-8eb9-cf517e5c4fe8"), new TimeSpan(0, 18, 0, 0, 0), "Special Event 1" });

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 12,
                column: "RestaurantId",
                value: new Guid("1604f79d-c4a9-4413-9708-76a07686370d"));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 13,
                column: "RestaurantId",
                value: new Guid("1604f79d-c4a9-4413-9708-76a07686370d"));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 14,
                column: "RestaurantId",
                value: new Guid("1604f79d-c4a9-4413-9708-76a07686370d"));
        }
    }
}

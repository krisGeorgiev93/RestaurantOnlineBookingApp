using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class SeedOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7b229258-a86c-46e4-b2b8-f574c90e6419"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7bf2107b-f7ac-4292-9eb9-d2a26babc73c"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("f1d9b41b-d74c-4109-ac1b-a87594189cbd"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("8099b56d-7710-415d-9c06-4569082c6758"), 0, "fd77d7af-0ffe-4256-a990-5d77c4fb68f3", "ivan@mail.com", false, "Ivan", "Georgiev", false, null, "ivan@mail.com", "ivan@mail.com", "AQAAAAEAACcQAAAAENMVNddACCeNLbeieHyle/nPx5zrnWycy/ujHcgcj+7F5j8N8WXPV/Q7vWA68ukvTQ==", null, false, null, false, "ivan@mail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("faf6dc41-ce01-44a9-b63c-0abd2df2d15f"), 0, "c84a288d-764e-472f-97ca-9e49793f1cb8", "kiril@mail.com", false, "Kiril", "Ivanov", false, null, "kiril@mail.com", "kiril@mail.com", "AQAAAAEAACcQAAAAEB1mp2YDyuhfXDT98v5KyOM3vm1BCm4q9Zw/FB7wH3qc5DZTz9nUNJ/21RNTJYEmkg==", null, false, null, false, "kiril@mail.com" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "Description", "ImageUrl", "Price", "RestaurantId", "Time", "Title" },
                values: new object[] { new Guid("1c17cc67-1fb5-4090-87c9-888b1db8669e"), new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Local), "Description for Special Event 1", "https://www.eventbookings.com/wp-content/uploads/2018/03/event-ideas-for-party-eventbookings.jpg", 50.00m, new Guid("e681a1dd-b85f-4860-8eb9-cf517e5c4fe8"), new TimeSpan(0, 18, 0, 0, 0), "Special Event 1" });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[] { new Guid("44e7b2ef-dfa8-45ae-aca9-0b52b9a3df4d"), "+359888888888", new Guid("faf6dc41-ce01-44a9-b63c-0abd2df2d15f") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8099b56d-7710-415d-9c06-4569082c6758"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("1c17cc67-1fb5-4090-87c9-888b1db8669e"));

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: new Guid("44e7b2ef-dfa8-45ae-aca9-0b52b9a3df4d"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("faf6dc41-ce01-44a9-b63c-0abd2df2d15f"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("7b229258-a86c-46e4-b2b8-f574c90e6419"), 0, "c680007e-f7cc-4600-b7b0-de11723c87a6", "ivan@mail.com", false, "Ivan", "Georgiev", false, null, "ivan@mail.com", "ivan@mail.com", "AQAAAAEAACcQAAAAEBhXzTIXjmopq12RRt9EjjuMGhIr4vcxOVaFU3oIzHngnXG+w87Yd/d1F0+qHjO44g==", null, false, null, false, "ivan@mail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("7bf2107b-f7ac-4292-9eb9-d2a26babc73c"), 0, "d17e3cbb-dc97-4c2c-9254-551610431e49", "kiril@mail.com", false, "Kiril", "Ivanov", false, null, "kiril@mail.com", "kiril@mail.com", "AQAAAAEAACcQAAAAEPrgQ1v9sDC2k9DPZ9skH0hpYY1DxuMAdquKeKi7off9YwYzu2JUX9ae5tpwZOFtjA==", null, false, null, false, "kiril@mail.com" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "Description", "ImageUrl", "Price", "RestaurantId", "Time", "Title" },
                values: new object[] { new Guid("f1d9b41b-d74c-4109-ac1b-a87594189cbd"), new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Local), "Description for Special Event 1", "https://www.eventbookings.com/wp-content/uploads/2018/03/event-ideas-for-party-eventbookings.jpg", 50.00m, new Guid("e681a1dd-b85f-4860-8eb9-cf517e5c4fe8"), new TimeSpan(0, 18, 0, 0, 0), "Special Event 1" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class SeedUsers2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("05e79990-5df5-49b4-b7c2-68976a6d12c3"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("e3635f2b-ecaf-487f-8134-bb0d7cebd5b8"));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { new Guid("05e79990-5df5-49b4-b7c2-68976a6d12c3"), 0, "222747e6-a579-46ef-ae11-b5cbedbf1bd8", "ivan@mail.com", false, "Ivan", "Georgiev", false, null, "ivan@mail.com", "ivan@mail.com", "AQAAAAEAACcQAAAAEN8VO7Lfq6WJGz6Mzi/CqvhMcHkNY+r/AB9Avb0Bpr0k5imeGPiZOe+cskrSIgNVng==", null, false, null, false, "ivan@mail.com" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "Description", "ImageUrl", "Price", "RestaurantId", "Time", "Title" },
                values: new object[] { new Guid("e3635f2b-ecaf-487f-8134-bb0d7cebd5b8"), new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Local), "Description for Special Event 1", "https://www.eventbookings.com/wp-content/uploads/2018/03/event-ideas-for-party-eventbookings.jpg", 50.00m, new Guid("e681a1dd-b85f-4860-8eb9-cf517e5c4fe8"), new TimeSpan(0, 18, 0, 0, 0), "Special Event 1" });
        }
    }
}

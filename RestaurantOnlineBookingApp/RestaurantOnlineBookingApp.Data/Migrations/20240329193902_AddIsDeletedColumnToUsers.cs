using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class AddIsDeletedColumnToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("1771c345-1f92-4e0a-b40f-dd9954f1dba6"));

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "AspNetUsers",
                newName: "IsDeleted");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8099b56d-7710-415d-9c06-4569082c6758"),
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "PasswordHash" },
                values: new object[] { "9ec6d2d7-82be-4bc2-ae7c-40b5e4767970", false, "AQAAAAEAACcQAAAAEJmIbDOlQLffkX2MGlgdByLSD9ajvYOb0kG6FmWAZMP4kqx7XMp3iCDczVkd7ZtH4A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("faf6dc41-ce01-44a9-b63c-0abd2df2d15f"),
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "PasswordHash" },
                values: new object[] { "1616d9ab-4e5e-4a1a-ae02-1b5060a7a699", false, "AQAAAAEAACcQAAAAEGuksS+G8cNadYw70hX7ylzsx/9WvAwmjIEn1XcLPvKyAUpRJsF1ngwf8UJQNBvxmg==" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "Description", "ImageUrl", "Price", "RestaurantId", "Time", "Title" },
                values: new object[] { new Guid("6f02c324-afe6-4ef3-9ef0-69d061baaeb0"), new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), "Description for Special Event 1", "https://www.eventbookings.com/wp-content/uploads/2018/03/event-ideas-for-party-eventbookings.jpg", 50.00m, new Guid("e681a1dd-b85f-4860-8eb9-cf517e5c4fe8"), new TimeSpan(0, 18, 0, 0, 0), "Special Event 1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("6f02c324-afe6-4ef3-9ef0-69d061baaeb0"));

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "AspNetUsers",
                newName: "IsActive");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8099b56d-7710-415d-9c06-4569082c6758"),
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash" },
                values: new object[] { "fe968d97-a990-4b8f-a03c-32a319ec63ea", true, "AQAAAAEAACcQAAAAEIcKe2AlqFGgx3oedozTCv/Y6+FApNCtIVKy5HQm0Enm/cqHayDVU8R+5OCuV6NyQg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("faf6dc41-ce01-44a9-b63c-0abd2df2d15f"),
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash" },
                values: new object[] { "825b1315-963a-4bd2-80f7-adf802cc9bf0", true, "AQAAAAEAACcQAAAAEI93Jh1wkuPcEV/d1GKUAgjxqqRBHNhM0kmT9+koqdKE8zszfLJdYtD0Kbv6l4IJeA==" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "Description", "ImageUrl", "Price", "RestaurantId", "Time", "Title" },
                values: new object[] { new Guid("1771c345-1f92-4e0a-b40f-dd9954f1dba6"), new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Local), "Description for Special Event 1", "https://www.eventbookings.com/wp-content/uploads/2018/03/event-ideas-for-party-eventbookings.jpg", 50.00m, new Guid("e681a1dd-b85f-4860-8eb9-cf517e5c4fe8"), new TimeSpan(0, 18, 0, 0, 0), "Special Event 1" });
        }
    }
}

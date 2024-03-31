using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class AddPhotoEntityToRestaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Restaurants_RestaurantId",
                table: "Photo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photo",
                table: "Photo");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("f5285334-a41e-454c-87ff-c8c5138495bc"));

            migrationBuilder.RenameTable(
                name: "Photo",
                newName: "Photos");

            migrationBuilder.RenameIndex(
                name: "IX_Photo_RestaurantId",
                table: "Photos",
                newName: "IX_Photos_RestaurantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photos",
                table: "Photos",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8099b56d-7710-415d-9c06-4569082c6758"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dc57ed3a-fce0-4be9-ac70-a7a2fe206d3f", "AQAAAAEAACcQAAAAEF1gFsuEul/KqIk55E//8C3WX1YcglseN4ym3VSrUUAUJWZfb1M2SQl33Ob2deWQQA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("faf6dc41-ce01-44a9-b63c-0abd2df2d15f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d8886593-9818-44cf-a36c-2e41ce0f88ad", "AQAAAAEAACcQAAAAEA05LxrpgUIpJ8hPtTlgzrY6GMvUuuQlo7DnaEaXBbI5kCnFxhf+YwJhNdyOCwFrZQ==" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "Description", "ImageUrl", "Price", "RestaurantId", "Time", "Title" },
                values: new object[] { new Guid("54d0f560-eebd-4937-877f-0f9892bb392a"), new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Local), "Description for Special Event 1", "https://www.eventbookings.com/wp-content/uploads/2018/03/event-ideas-for-party-eventbookings.jpg", 50.00m, new Guid("e681a1dd-b85f-4860-8eb9-cf517e5c4fe8"), new TimeSpan(0, 18, 0, 0, 0), "Special Event 1" });

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Restaurants_RestaurantId",
                table: "Photos",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Restaurants_RestaurantId",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photos",
                table: "Photos");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("54d0f560-eebd-4937-877f-0f9892bb392a"));

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "Photo");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_RestaurantId",
                table: "Photo",
                newName: "IX_Photo_RestaurantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photo",
                table: "Photo",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8099b56d-7710-415d-9c06-4569082c6758"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "78f36829-d548-456d-b308-f21f75068993", "AQAAAAEAACcQAAAAEFGoH7jGE4JXnkq0vBxmStcVQIGsrR3V5xz+WTVhOazZnEBlWEt5FUZE+GTGY4EUlg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("faf6dc41-ce01-44a9-b63c-0abd2df2d15f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fa067820-4413-45c4-87b5-4edb5ab6016e", "AQAAAAEAACcQAAAAEAdDfW4ispKHnMQugahVSVVhrWyqMl19QUJR5FyQ72vZ53CnO96Z5Z75KND/INO8hw==" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "Description", "ImageUrl", "Price", "RestaurantId", "Time", "Title" },
                values: new object[] { new Guid("f5285334-a41e-454c-87ff-c8c5138495bc"), new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Local), "Description for Special Event 1", "https://www.eventbookings.com/wp-content/uploads/2018/03/event-ideas-for-party-eventbookings.jpg", 50.00m, new Guid("e681a1dd-b85f-4860-8eb9-cf517e5c4fe8"), new TimeSpan(0, 18, 0, 0, 0), "Special Event 1" });

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Restaurants_RestaurantId",
                table: "Photo",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

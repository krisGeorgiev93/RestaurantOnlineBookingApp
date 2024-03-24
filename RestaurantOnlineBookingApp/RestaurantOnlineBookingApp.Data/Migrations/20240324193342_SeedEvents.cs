using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class SeedEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "Description", "ImageUrl", "Price", "RestaurantId", "Time", "Title" },
                values: new object[] { new Guid("550a3e7f-28e4-42e9-986b-3939627f61f5"), new DateTime(2024, 3, 29, 0, 0, 0, 0, DateTimeKind.Local), "Description for Special Event 1", "https://www.eventbookings.com/wp-content/uploads/2018/03/event-ideas-for-party-eventbookings.jpg", 50.00m, new Guid("e681a1dd-b85f-4860-8eb9-cf517e5c4fe8"), new TimeSpan(0, 18, 0, 0, 0), "Special Event 1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("550a3e7f-28e4-42e9-986b-3939627f61f5"));
        }
    }
}

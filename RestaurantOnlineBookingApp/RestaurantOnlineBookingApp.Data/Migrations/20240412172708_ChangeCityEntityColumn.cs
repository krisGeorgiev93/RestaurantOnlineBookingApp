using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class ChangeCityEntityColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("982191da-2992-4e02-933b-2386954d47e5"));

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Cities");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("416a7e0d-3ba0-45b8-8613-8d00e239b3f0"));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8099b56d-7710-415d-9c06-4569082c6758"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "36a030db-8b84-4d33-9184-3f9ac72a0cb4", "AQAAAAEAACcQAAAAEC7Fc667oTkfVsn1UNj1bSDVBjkqs5ZYZHwJE8q2KY+4ibSnGH2HPc3RhzoMmcD1JA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("faf6dc41-ce01-44a9-b63c-0abd2df2d15f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "953fd01e-94e7-49c2-82a5-b820ac49d444", "AQAAAAEAACcQAAAAENpUL9LnHEGRqgXA5KU+Syylx3WC2E8GCzJJ8kkWC6RHTB8LHLgP2+VgiXCInpbTsg==" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cc/Sofia_333.jpg/800px-Sofia_333.jpg");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://i.guim.co.uk/img/media/1e6a94ecca5c1df6696f6673fe657e5d16819933/366_620_5634_3380/master/5634.jpg?width=1900&dpr=2&s=none");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://content.r9cdn.net/rimg/dimg/e5/ce/ad8df304-city-12778-1656216d7ab.jpg?width=2160&height=1215&xhint=2049&yhint=1054&crop=true&outputtype=webp");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://www.kayak.co.uk/rimg/himg/33/36/89/expediav2-72370-760be8-606434.jpg?width=2160&height=1215&crop=true&outputtype=webp");

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "Description", "ImageUrl", "Price", "RestaurantId", "Time", "Title" },
                values: new object[] { new Guid("982191da-2992-4e02-933b-2386954d47e5"), new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Local), "Description for Special Event 1", "https://www.eventbookings.com/wp-content/uploads/2018/03/event-ideas-for-party-eventbookings.jpg", 50.00m, new Guid("e681a1dd-b85f-4860-8eb9-cf517e5c4fe8"), new TimeSpan(0, 18, 0, 0, 0), "Special Event 1" });
        }
    }
}

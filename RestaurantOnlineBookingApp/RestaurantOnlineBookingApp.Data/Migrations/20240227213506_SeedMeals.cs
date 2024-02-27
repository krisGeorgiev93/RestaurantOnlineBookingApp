using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class SeedMeals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("16937200-8a94-4e3b-a787-72051b70efbc"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("dea7370f-5095-4e6e-b112-3995096f8f01"));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Meals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "Italian pizza with cheese and peperoni", "https://www.simplyrecipes.com/thmb/KE6iMblr3R2Db6oE8HdyVsFSj2A=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/__opt__aboutcom__coeus__resources__content_migration__simply_recipes__uploads__2019__09__easy-pepperoni-pizza-lead-3-1024x682-583b275444104ef189d693a64df625da.jpg", "Pizza Peperoni", 10.5, new Guid("1604f79d-c4a9-4413-9708-76a07686370d") },
                    { 2, "Chinese chicken noodles with eggs and onion", "https://sinfullyspicy.com/wp-content/uploads/2023/01/1200-by-1200-images-5-500x375.jpg", "Chicken Noodles", 7.5, new Guid("1604f79d-c4a9-4413-9708-76a07686370d") },
                    { 3, "Chinese chicken fried rice with eggs", "https://tildaricelive.s3.eu-central-1.amazonaws.com/wp-content/uploads/2021/05/04111234/chicken-fried-rice-low-res-2.png", "Chicken Fried Rice", 9.1999999999999993, new Guid("1604f79d-c4a9-4413-9708-76a07686370d") }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "EndingTime", "GuestId", "ImageUrl", "Name", "OwnerId", "StartingTime" },
                values: new object[,]
                {
                    { new Guid("5507e5b9-562a-4cae-849a-7aa911cdd8a9"), "Hristo Botev 76", 50, 6, 1, "Best chinese in the country", new TimeSpan(0, 23, 0, 0, 0), null, "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg", "Best Of China", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 18, 0, 0, 0) },
                    { new Guid("6c3e8517-546b-48b2-bc1b-95e0b5006ebd"), "Ivan Ivanov 26", 100, 2, 1, "Best food from Asia", new TimeSpan(0, 23, 30, 0, 0), null, "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg", "Asian Buffet", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 17, 0, 0, 0) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("5507e5b9-562a-4cae-849a-7aa911cdd8a9"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("6c3e8517-546b-48b2-bc1b-95e0b5006ebd"));

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Meals");

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "EndingTime", "GuestId", "ImageUrl", "IsActive", "Name", "OwnerId", "StartingTime" },
                values: new object[] { new Guid("16937200-8a94-4e3b-a787-72051b70efbc"), "Hristo Botev 76", 50, 6, 1, "Best chinese in the country", new TimeSpan(0, 23, 0, 0, 0), null, "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg", false, "Best Of China", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 18, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "EndingTime", "GuestId", "ImageUrl", "IsActive", "Name", "OwnerId", "StartingTime" },
                values: new object[] { new Guid("dea7370f-5095-4e6e-b112-3995096f8f01"), "Ivan Ivanov 26", 100, 2, 1, "Best food from Asia", new TimeSpan(0, 23, 30, 0, 0), null, "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg", false, "Asian Buffet", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 17, 0, 0, 0) });
        }
    }
}

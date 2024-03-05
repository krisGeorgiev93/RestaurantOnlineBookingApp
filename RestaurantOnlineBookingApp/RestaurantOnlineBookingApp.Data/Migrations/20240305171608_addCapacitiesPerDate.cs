using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class addCapacitiesPerDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("0ffc6e4c-98a6-427b-adc5-ddbaaee5050c"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("2c5ac627-3668-40c6-afad-061f529ad32c"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("a20bcba7-b77d-463d-89cc-01e411008e84"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("df9750d5-b25c-4046-bf29-2c563d0b56ff"));

            migrationBuilder.CreateTable(
                name: "CapacitiesParDate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapacitiesParDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CapacitiesParDate_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "EndingTime", "GuestId", "ImageUrl","IsActive", "Name", "OwnerId", "StartingTime" },
                values: new object[,]
                {
                    { new Guid("2b11f6db-5a55-462e-a32c-e239b2757c63"), "Hristo Hristov 74", 150, 1, 2, "Traditional food from Bulgarian Kitchen", new TimeSpan(0, 23, 45, 0, 0), null, "https://media-cdn.tripadvisor.com/media/photo-s/03/b7/8b/ed/chevermeto-traditional.jpg",true, "Bulgarian Taste", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 12, 0, 0, 0) },
                    { new Guid("43e939b3-753d-4cb1-a6b9-144ec851dc91"), "Hristo Botev 76", 50, 6, 1, "Best chinese in the country", new TimeSpan(0, 23, 0, 0, 0), null, "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg",true, "Best Of China", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 18, 0, 0, 0) },
                    { new Guid("9263ad29-840f-4c01-a54e-287a3a00ba5e"), "Georgi Ivanov 26", 135, 4, 3, "Best food from Turkey", new TimeSpan(0, 23, 30, 0, 0), null, "https://images.squarespace-cdn.com/content/v1/62f4e2e25f95bb5d35522adc/0c8e42d8-aa26-4c9e-acbc-bcfd336b3731/Havin_a_Turkish_X_socialawakening+18.jpg",true, "Turkish Restaurant", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 12, 0, 0, 0) },
                    { new Guid("c0278afa-b978-47cc-a0d1-2e417ba3628c"), "Ivan Ivanov 26", 100, 2, 1, "Best food from Asia", new TimeSpan(0, 23, 30, 0, 0), null, "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg",true, "Asian Buffet", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 17, 0, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CapacitiesParDate_RestaurantId",
                table: "CapacitiesParDate",
                column: "RestaurantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CapacitiesParDate");

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("2b11f6db-5a55-462e-a32c-e239b2757c63"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("43e939b3-753d-4cb1-a6b9-144ec851dc91"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("9263ad29-840f-4c01-a54e-287a3a00ba5e"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("c0278afa-b978-47cc-a0d1-2e417ba3628c"));

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Capacity", "CategoryId", "CityId", "Description", "EndingTime", "GuestId", "ImageUrl", "IsActive", "Name", "OwnerId", "StartingTime" },
                values: new object[,]
                {
                    { new Guid("0ffc6e4c-98a6-427b-adc5-ddbaaee5050c"), "Ivan Ivanov 26", 100, 2, 1, "Best food from Asia", new TimeSpan(0, 23, 30, 0, 0), null, "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg", false, "Asian Buffet", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 17, 0, 0, 0) },
                    { new Guid("2c5ac627-3668-40c6-afad-061f529ad32c"), "Hristo Hristov 74", 150, 1, 2, "Traditional food from Bulgarian Kitchen", new TimeSpan(0, 23, 45, 0, 0), null, "https://media-cdn.tripadvisor.com/media/photo-s/03/b7/8b/ed/chevermeto-traditional.jpg", false, "Bulgarian Taste", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 12, 0, 0, 0) },
                    { new Guid("a20bcba7-b77d-463d-89cc-01e411008e84"), "Georgi Ivanov 26", 135, 4, 3, "Best food from Turkey", new TimeSpan(0, 23, 30, 0, 0), null, "https://images.squarespace-cdn.com/content/v1/62f4e2e25f95bb5d35522adc/0c8e42d8-aa26-4c9e-acbc-bcfd336b3731/Havin_a_Turkish_X_socialawakening+18.jpg", false, "Turkish Restaurant", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 12, 0, 0, 0) },
                    { new Guid("df9750d5-b25c-4046-bf29-2c563d0b56ff"), "Hristo Botev 76", 50, 6, 1, "Best chinese in the country", new TimeSpan(0, 23, 0, 0, 0), null, "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg", false, "Best Of China", new Guid("c4f8569c-1cda-4b0b-94e4-16b44a4631cf"), new TimeSpan(0, 18, 0, 0, 0) }
                });
        }
    }
}

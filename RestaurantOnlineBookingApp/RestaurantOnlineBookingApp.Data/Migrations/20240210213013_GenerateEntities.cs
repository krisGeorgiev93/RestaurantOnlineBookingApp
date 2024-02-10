using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class GenerateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "Id", "CityName", "ImageUrl" },
                values: new object[,]
                {
                    { 1, "Sofia", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cc/Sofia_333.jpg/800px-Sofia_333.jpg" },
                    { 2, "Plovdiv", "https://i.guim.co.uk/img/media/1e6a94ecca5c1df6696f6673fe657e5d16819933/366_620_5634_3380/master/5634.jpg?width=1900&dpr=2&s=none" },
                    { 3, "Varna", "https://content.r9cdn.net/rimg/dimg/e5/ce/ad8df304-city-12778-1656216d7ab.jpg?width=2160&height=1215&xhint=2049&yhint=1054&crop=true&outputtype=webp" },
                    { 4, "Burgas", "https://www.kayak.co.uk/rimg/himg/33/36/89/expediav2-72370-760be8-606434.jpg?width=2160&height=1215&crop=true&outputtype=webp" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "CategoryId", "CityId", "Description", "ImageUrl", "Name", "OwnerId", "Rating" },
                values: new object[] { new Guid("0d2baa67-dce0-4a77-a1f8-614f7cbbfecc"), "Hristo Botev 76", 6, 1, "Best chinese in the country", "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg", "Best Of China", new Guid("92bc2551-2565-4fc4-8ed6-98deffe533b9"), 5.0 });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "CategoryId", "CityId", "Description", "ImageUrl", "Name", "OwnerId", "Rating" },
                values: new object[] { new Guid("cd2e7147-2adb-4c5a-9563-c2d9077dfa7c"), "Ivan Vazov 26", 2, 1, "Best food from Asia", "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg", "Asian Buffet", new Guid("92bc2551-2565-4fc4-8ed6-98deffe533b9"), 4.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

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
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("0d2baa67-dce0-4a77-a1f8-614f7cbbfecc"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("cd2e7147-2adb-4c5a-9563-c2d9077dfa7c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

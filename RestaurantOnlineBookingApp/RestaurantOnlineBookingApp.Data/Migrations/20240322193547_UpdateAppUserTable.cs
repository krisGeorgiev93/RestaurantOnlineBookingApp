using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class UpdateAppUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "Restaurants",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_AppUserId",
                table: "Restaurants",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_AppUserId",
                table: "Restaurants",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_AppUserId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_AppUserId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Restaurants");
        }
    }
}

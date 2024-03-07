using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class UpdateEventTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Meals_MealId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_MealId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "Events");

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Meals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RestaurantId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Meals_EventId",
                table: "Meals",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_RestaurantId",
                table: "Events",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Restaurants_RestaurantId",
                table: "Events",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Events_EventId",
                table: "Meals",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Restaurants_RestaurantId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Events_EventId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_EventId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Events_RestaurantId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "MealId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Events_MealId",
                table: "Events",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Meals_MealId",
                table: "Events",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

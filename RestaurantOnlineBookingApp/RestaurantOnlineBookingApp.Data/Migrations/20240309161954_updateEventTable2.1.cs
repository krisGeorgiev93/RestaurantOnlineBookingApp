using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOnlineBookingApp.Data.Migrations
{
    public partial class updateEventTable21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Events_EventId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Events_EventId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_EventId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_EventId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Bookings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Meals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meals_EventId",
                table: "Meals",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_EventId",
                table: "Bookings",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Events_EventId",
                table: "Bookings",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Events_EventId",
                table: "Meals",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }
    }
}

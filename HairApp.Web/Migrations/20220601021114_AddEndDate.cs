using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HairApp.Web.Migrations
{
    public partial class AddEndDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Addrees",
                table: "BookingHistories");

            migrationBuilder.RenameColumn(
                name: "DateLocal",
                table: "BookingHistories",
                newName: "EndDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Bookings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "BookingHistories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "BookingHistories");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "BookingHistories",
                newName: "DateLocal");

            migrationBuilder.AddColumn<string>(
                name: "Addrees",
                table: "BookingHistories",
                nullable: true);
        }
    }
}

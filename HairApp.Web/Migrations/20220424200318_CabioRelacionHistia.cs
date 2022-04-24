using Microsoft.EntityFrameworkCore.Migrations;

namespace HairApp.Web.Migrations
{
    public partial class CabioRelacionHistia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Bookings_BookingId",
                table: "Rates");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Rates",
                newName: "BookingHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Rates_BookingId",
                table: "Rates",
                newName: "IX_Rates_BookingHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_BookingHistories_BookingHistoryId",
                table: "Rates",
                column: "BookingHistoryId",
                principalTable: "BookingHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_BookingHistories_BookingHistoryId",
                table: "Rates");

            migrationBuilder.RenameColumn(
                name: "BookingHistoryId",
                table: "Rates",
                newName: "BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Rates_BookingHistoryId",
                table: "Rates",
                newName: "IX_Rates_BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Bookings_BookingId",
                table: "Rates",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace HairApp.Web.Migrations
{
    public partial class Cambiosp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Shops_ShopId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Neighborhoods_NeighborhoodId",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Shops_NeighborhoodId",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Services_ShopId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "NeighborhoodId",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Services");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NeighborhoodId",
                table: "Shops",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "Services",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shops_NeighborhoodId",
                table: "Shops",
                column: "NeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ShopId",
                table: "Services",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Shops_ShopId",
                table: "Services",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Neighborhoods_NeighborhoodId",
                table: "Shops",
                column: "NeighborhoodId",
                principalTable: "Neighborhoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace HairApp.Web.Migrations
{
    public partial class CambioRelacionShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Neighborhoods_Shops_ShopId",
                table: "Neighborhoods");

            migrationBuilder.DropIndex(
                name: "IX_Neighborhoods_ShopId",
                table: "Neighborhoods");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Neighborhoods");

            migrationBuilder.AddColumn<int>(
                name: "NeighborhoodsId",
                table: "Shops",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "Services",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shops_NeighborhoodsId",
                table: "Shops",
                column: "NeighborhoodsId");

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
                name: "FK_Shops_Neighborhoods_NeighborhoodsId",
                table: "Shops",
                column: "NeighborhoodsId",
                principalTable: "Neighborhoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Shops_ShopId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Neighborhoods_NeighborhoodsId",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Shops_NeighborhoodsId",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Services_ShopId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "NeighborhoodsId",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "Neighborhoods",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Neighborhoods_ShopId",
                table: "Neighborhoods",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Neighborhoods_Shops_ShopId",
                table: "Neighborhoods",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

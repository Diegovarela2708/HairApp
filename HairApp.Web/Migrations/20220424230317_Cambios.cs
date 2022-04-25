using Microsoft.EntityFrameworkCore.Migrations;

namespace HairApp.Web.Migrations
{
    public partial class Cambios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Neighborhoods_NeighborhoodsId",
                table: "Shops");

            migrationBuilder.RenameColumn(
                name: "NeighborhoodsId",
                table: "Shops",
                newName: "NeighborhoodId");

            migrationBuilder.RenameIndex(
                name: "IX_Shops_NeighborhoodsId",
                table: "Shops",
                newName: "IX_Shops_NeighborhoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Neighborhoods_NeighborhoodId",
                table: "Shops",
                column: "NeighborhoodId",
                principalTable: "Neighborhoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Neighborhoods_NeighborhoodId",
                table: "Shops");

            migrationBuilder.RenameColumn(
                name: "NeighborhoodId",
                table: "Shops",
                newName: "NeighborhoodsId");

            migrationBuilder.RenameIndex(
                name: "IX_Shops_NeighborhoodId",
                table: "Shops",
                newName: "IX_Shops_NeighborhoodsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Neighborhoods_NeighborhoodsId",
                table: "Shops",
                column: "NeighborhoodsId",
                principalTable: "Neighborhoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

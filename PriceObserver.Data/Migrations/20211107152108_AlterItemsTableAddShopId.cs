using Microsoft.EntityFrameworkCore.Migrations;

namespace PriceObserver.Data.Migrations
{
    public partial class AlterItemsTableAddShopId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "Items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ShopId",
                table: "Items",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Shops_ShopId",
                table: "Items",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Shops_ShopId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ShopId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Items");
        }
    }
}

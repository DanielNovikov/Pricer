using Microsoft.EntityFrameworkCore.Migrations;

namespace PriceObserver.Data.Migrations
{
    public partial class AlterMenusTableAddResourceKeyColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Menus");

            migrationBuilder.AddColumn<int>(
                name: "ResourceKey",
                table: "Menus",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResourceKey",
                table: "Menus");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Menus",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }
    }
}

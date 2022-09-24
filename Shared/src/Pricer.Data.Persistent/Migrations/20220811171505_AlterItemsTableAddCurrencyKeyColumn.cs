using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pricer.Data.Migrations
{
    public partial class AlterItemsTableAddCurrencyKeyColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyKey",
                table: "Items",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyKey",
                table: "Items");
        }
    }
}

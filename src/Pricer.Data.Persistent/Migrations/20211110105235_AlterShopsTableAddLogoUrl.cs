using Microsoft.EntityFrameworkCore.Migrations;

namespace Pricer.Data.Migrations
{
    public partial class AlterShopsTableAddLogoUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Shops",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Shops");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pricer.Data.Migrations
{
    public partial class AlterUserTableAddSelectedLanguageKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectedLanguageKey",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedLanguageKey",
                table: "Users");
        }
    }
}

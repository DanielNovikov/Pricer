using Microsoft.EntityFrameworkCore.Migrations;

namespace Pricer.Data.Migrations
{
    public partial class AlterUsersTableRemoveCanAwaitInputColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanAwaitInput",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanAwaitInput",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

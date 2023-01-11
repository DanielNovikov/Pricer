using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pricer.Data.Migrations
{
    public partial class AlterUsersTableAddGrowthPriceNotificationsEnabledColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GrowthPriceNotificationsEnabled",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrowthPriceNotificationsEnabled",
                table: "Users");
        }
    }
}

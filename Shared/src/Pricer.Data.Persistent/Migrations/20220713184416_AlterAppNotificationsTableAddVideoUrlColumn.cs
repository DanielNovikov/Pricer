using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pricer.Data.Migrations
{
    public partial class AlterAppNotificationsTableAddVideoUrlColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "AppNotifications");

            migrationBuilder.AddColumn<int>(
                name: "Content",
                table: "AppNotifications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "AppNotifications",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "AppNotifications");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "AppNotifications");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "AppNotifications",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

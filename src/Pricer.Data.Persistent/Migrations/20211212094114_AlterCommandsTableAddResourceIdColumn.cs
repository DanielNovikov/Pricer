using Microsoft.EntityFrameworkCore.Migrations;

namespace Pricer.Data.Migrations
{
    public partial class AlterCommandsTableAddResourceIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Commands");

            migrationBuilder.AddColumn<int>(
                name: "ResourceId",
                table: "Commands",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Commands_ResourceId",
                table: "Commands",
                column: "ResourceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Commands_Resources_ResourceId",
                table: "Commands",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commands_Resources_ResourceId",
                table: "Commands");

            migrationBuilder.DropIndex(
                name: "IX_Commands_ResourceId",
                table: "Commands");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "Commands");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Commands",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceObserver.Data.Migrations
{
    public partial class AlterUserTokensTableAddExpirationColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expired",
                table: "UserTokens");
            
            migrationBuilder.Sql("TRUNCATE \"UserTokens\" RESTART IDENTITY;");

            migrationBuilder.AddColumn<DateTime>(
                name: "Expiration",
                table: "UserTokens",
                type: "timestamp with time zone",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expiration",
                table: "UserTokens");

            migrationBuilder.AddColumn<bool>(
                name: "Expired",
                table: "UserTokens",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

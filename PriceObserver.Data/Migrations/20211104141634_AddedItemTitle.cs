﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace PriceObserver.Data.Migrations
{
    public partial class AddedItemTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Items",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Items");
        }
    }
}

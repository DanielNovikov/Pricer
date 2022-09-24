using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceObserver.Data.Migrations
{
    public partial class AlterItemsTableAddIsAvailableColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Items",
                type: "boolean",
                nullable: true);

            migrationBuilder.Sql(@"
UPDATE public.""Items""
SET ""IsAvailable"" = true;

ALTER TABLE public.""Items"" 
ALTER COLUMN ""IsAvailable"" SET NOT NULL;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Items");
        }
    }
}

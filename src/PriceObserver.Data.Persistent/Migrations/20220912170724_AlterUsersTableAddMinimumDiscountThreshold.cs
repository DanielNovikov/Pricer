using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PriceObserver.Data.Migrations
{
    public partial class AlterUsersTableAddMinimumDiscountThreshold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinimumDiscountThreshold",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("UPDATE public.\"Users\" SET \"MinimumDiscountThreshold\" = 5");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimumDiscountThreshold",
                table: "Users");
        }
    }
}

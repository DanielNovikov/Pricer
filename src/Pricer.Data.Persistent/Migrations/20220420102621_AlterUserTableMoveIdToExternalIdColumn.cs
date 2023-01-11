using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Pricer.Data.Migrations
{
    public partial class AlterUserTableMoveIdToExternalIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            const string query = @"
ALTER TABLE ""Items""
DROP CONSTRAINT ""FK_Items_Users_UserId"";

ALTER TABLE ""UserTokens""
DROP CONSTRAINT ""FK_UserTokens_Users_UserId"";

ALTER TABLE ""Users""
DROP CONSTRAINT ""PK_Users"";

ALTER TABLE ""Users""
RENAME COLUMN ""Id"" TO ""ExternalId"";

ALTER TABLE ""Users""
ADD COLUMN ""Id"" SERIAL PRIMARY KEY;

CREATE INDEX IX_Users_ExternalId 
ON ""Users"" (""ExternalId"");

ALTER TABLE ""UserTokens""
ALTER COLUMN ""UserId"" TYPE INTEGER;

ALTER TABLE ""Items""
ALTER COLUMN ""UserId"" TYPE INTEGER;

UPDATE ""UserTokens"" AS ut
SET ""UserId"" = u.""Id""
FROM ""Users"" AS u
WHERE u.""ExternalId"" = ut.""UserId"";

UPDATE ""Items"" AS i
SET ""UserId"" = u.""Id""
FROM ""Users"" AS u
WHERE u.""ExternalId"" = i.""UserId"";

ALTER TABLE ""Items""
ADD CONSTRAINT ""FK_Items_Users_UserId"" 
FOREIGN KEY (""UserId"") REFERENCES ""Users"" (""Id"");";

            migrationBuilder.Sql(query);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_ExternalId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Users");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "UserTokens",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Users",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Items",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}

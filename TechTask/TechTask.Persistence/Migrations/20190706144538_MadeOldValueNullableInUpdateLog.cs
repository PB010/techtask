using Microsoft.EntityFrameworkCore.Migrations;

namespace TechTask.Persistence.Migrations
{
    public partial class MadeOldValueNullableInUpdateLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OldValue",
                table: "UpdateLogs",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OldValue",
                table: "UpdateLogs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

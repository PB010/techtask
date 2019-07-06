using Microsoft.EntityFrameworkCore.Migrations;

namespace TechTask.Persistence.Migrations
{
    public partial class ChangedDbLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewValue",
                table: "UpdateLogs");

            migrationBuilder.RenameColumn(
                name: "OldValue",
                table: "UpdateLogs",
                newName: "Value");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "UpdateLogs",
                newName: "OldValue");

            migrationBuilder.AddColumn<string>(
                name: "NewValue",
                table: "UpdateLogs",
                nullable: false,
                defaultValue: "");
        }
    }
}

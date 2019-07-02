using Microsoft.EntityFrameworkCore.Migrations;

namespace TechTask.Persistence.Migrations
{
    public partial class UpdatedTaskModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrackerFirstName",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackerLastName",
                table: "Tasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackerFirstName",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TrackerLastName",
                table: "Tasks");
        }
    }
}

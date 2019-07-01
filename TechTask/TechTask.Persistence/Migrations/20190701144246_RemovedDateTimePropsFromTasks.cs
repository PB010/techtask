using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechTask.Persistence.Migrations
{
    public partial class RemovedDateTimePropsFromTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedTimeToFinish",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TotalHoursOfWork",
                table: "Tasks");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 17, 42, 46, 28, DateTimeKind.Local).AddTicks(5864), new DateTime(2019, 7, 1, 17, 42, 46, 31, DateTimeKind.Local).AddTicks(658) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 17, 42, 46, 31, DateTimeKind.Local).AddTicks(2930), new DateTime(2019, 7, 1, 17, 42, 46, 31, DateTimeKind.Local).AddTicks(2947) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("76aebd31-0235-4ef3-a123-08d6fbc1bdcd"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 17, 42, 46, 36, DateTimeKind.Local).AddTicks(5876), new DateTime(2019, 7, 1, 17, 42, 46, 36, DateTimeKind.Local).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bfd23b66-1a4e-41de-4aa7-08d6fae3b08b"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 17, 42, 46, 36, DateTimeKind.Local).AddTicks(3485), new DateTime(2019, 7, 1, 17, 42, 46, 36, DateTimeKind.Local).AddTicks(3516) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ed09fe47-84c0-47b5-8007-ae2ea4350a8b"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 17, 42, 46, 36, DateTimeKind.Local).AddTicks(7804), new DateTime(2019, 7, 1, 17, 42, 46, 36, DateTimeKind.Local).AddTicks(7813) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f3c88d42-fb42-43c4-a9d4-1a738a2bd20c"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 17, 42, 46, 36, DateTimeKind.Local).AddTicks(7858), new DateTime(2019, 7, 1, 17, 42, 46, 36, DateTimeKind.Local).AddTicks(7862) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedTimeToFinish",
                table: "Tasks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TotalHoursOfWork",
                table: "Tasks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 16, 21, 25, 184, DateTimeKind.Local).AddTicks(625), new DateTime(2019, 7, 1, 16, 21, 25, 185, DateTimeKind.Local).AddTicks(6079) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 16, 21, 25, 185, DateTimeKind.Local).AddTicks(8056), new DateTime(2019, 7, 1, 16, 21, 25, 185, DateTimeKind.Local).AddTicks(8074) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("76aebd31-0235-4ef3-a123-08d6fbc1bdcd"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 16, 21, 25, 191, DateTimeKind.Local).AddTicks(1716), new DateTime(2019, 7, 1, 16, 21, 25, 191, DateTimeKind.Local).AddTicks(1734) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bfd23b66-1a4e-41de-4aa7-08d6fae3b08b"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 16, 21, 25, 190, DateTimeKind.Local).AddTicks(9165), new DateTime(2019, 7, 1, 16, 21, 25, 190, DateTimeKind.Local).AddTicks(9200) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ed09fe47-84c0-47b5-8007-ae2ea4350a8b"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 16, 21, 25, 191, DateTimeKind.Local).AddTicks(3907), new DateTime(2019, 7, 1, 16, 21, 25, 191, DateTimeKind.Local).AddTicks(3921) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f3c88d42-fb42-43c4-a9d4-1a738a2bd20c"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 16, 21, 25, 191, DateTimeKind.Local).AddTicks(3957), new DateTime(2019, 7, 1, 16, 21, 25, 191, DateTimeKind.Local).AddTicks(3961) });
        }
    }
}

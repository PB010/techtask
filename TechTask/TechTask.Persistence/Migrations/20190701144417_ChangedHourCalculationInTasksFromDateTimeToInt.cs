using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechTask.Persistence.Migrations
{
    public partial class ChangedHourCalculationInTasksFromDateTimeToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstimatedTimeToFinish",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalHoursOfWork",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 17, 44, 16, 864, DateTimeKind.Local).AddTicks(8273), new DateTime(2019, 7, 1, 17, 44, 16, 866, DateTimeKind.Local).AddTicks(5766) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 17, 44, 16, 866, DateTimeKind.Local).AddTicks(8149), new DateTime(2019, 7, 1, 17, 44, 16, 866, DateTimeKind.Local).AddTicks(8167) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("76aebd31-0235-4ef3-a123-08d6fbc1bdcd"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 17, 44, 16, 872, DateTimeKind.Local).AddTicks(5492), new DateTime(2019, 7, 1, 17, 44, 16, 872, DateTimeKind.Local).AddTicks(5514) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bfd23b66-1a4e-41de-4aa7-08d6fae3b08b"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 17, 44, 16, 872, DateTimeKind.Local).AddTicks(1743), new DateTime(2019, 7, 1, 17, 44, 16, 872, DateTimeKind.Local).AddTicks(1783) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ed09fe47-84c0-47b5-8007-ae2ea4350a8b"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 17, 44, 16, 872, DateTimeKind.Local).AddTicks(8665), new DateTime(2019, 7, 1, 17, 44, 16, 872, DateTimeKind.Local).AddTicks(8682) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f3c88d42-fb42-43c4-a9d4-1a738a2bd20c"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 17, 44, 16, 872, DateTimeKind.Local).AddTicks(8736), new DateTime(2019, 7, 1, 17, 44, 16, 872, DateTimeKind.Local).AddTicks(8745) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}

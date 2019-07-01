using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechTask.Persistence.Migrations
{
    public partial class MadeTeamIdNonNullableInTaskTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Teams_TeamId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Tasks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 20, 39, 25, 730, DateTimeKind.Local).AddTicks(5233), new DateTime(2019, 7, 1, 20, 39, 25, 731, DateTimeKind.Local).AddTicks(2559) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 20, 39, 25, 731, DateTimeKind.Local).AddTicks(3590), new DateTime(2019, 7, 1, 20, 39, 25, 731, DateTimeKind.Local).AddTicks(3599) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 20, 39, 25, 731, DateTimeKind.Local).AddTicks(3612), new DateTime(2019, 7, 1, 20, 39, 25, 731, DateTimeKind.Local).AddTicks(3615) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 20, 39, 25, 734, DateTimeKind.Local).AddTicks(6783), new DateTime(2019, 7, 1, 20, 39, 25, 734, DateTimeKind.Local).AddTicks(6796) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 20, 39, 25, 734, DateTimeKind.Local).AddTicks(7836), new DateTime(2019, 7, 1, 20, 39, 25, 734, DateTimeKind.Local).AddTicks(7846) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("76aebd31-0235-4ef3-a123-08d6fbc1bdcd"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 20, 39, 25, 737, DateTimeKind.Local).AddTicks(8501), new DateTime(2019, 7, 1, 20, 39, 25, 737, DateTimeKind.Local).AddTicks(8511) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bfd23b66-1a4e-41de-4aa7-08d6fae3b08b"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 20, 39, 25, 737, DateTimeKind.Local).AddTicks(7064), new DateTime(2019, 7, 1, 20, 39, 25, 737, DateTimeKind.Local).AddTicks(7077) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ed09fe47-84c0-47b5-8007-ae2ea4350a8b"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 20, 39, 25, 737, DateTimeKind.Local).AddTicks(9730), new DateTime(2019, 7, 1, 20, 39, 25, 737, DateTimeKind.Local).AddTicks(9740) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f3c88d42-fb42-43c4-a9d4-1a738a2bd20c"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 20, 39, 25, 737, DateTimeKind.Local).AddTicks(9762), new DateTime(2019, 7, 1, 20, 39, 25, 737, DateTimeKind.Local).AddTicks(9766) });

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Teams_TeamId",
                table: "Tasks",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Teams_TeamId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 18, 9, 0, 996, DateTimeKind.Local).AddTicks(4660), new DateTime(2019, 7, 1, 18, 9, 0, 998, DateTimeKind.Local).AddTicks(1331) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 18, 9, 0, 998, DateTimeKind.Local).AddTicks(3652), new DateTime(2019, 7, 1, 18, 9, 0, 998, DateTimeKind.Local).AddTicks(3665) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 18, 9, 0, 998, DateTimeKind.Local).AddTicks(3692), new DateTime(2019, 7, 1, 18, 9, 0, 998, DateTimeKind.Local).AddTicks(3696) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 18, 9, 1, 5, DateTimeKind.Local).AddTicks(4989), new DateTime(2019, 7, 1, 18, 9, 1, 5, DateTimeKind.Local).AddTicks(5038) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 18, 9, 1, 5, DateTimeKind.Local).AddTicks(8148), new DateTime(2019, 7, 1, 18, 9, 1, 5, DateTimeKind.Local).AddTicks(8175) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("76aebd31-0235-4ef3-a123-08d6fbc1bdcd"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 18, 9, 1, 13, DateTimeKind.Local).AddTicks(5997), new DateTime(2019, 7, 1, 18, 9, 1, 13, DateTimeKind.Local).AddTicks(6019) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bfd23b66-1a4e-41de-4aa7-08d6fae3b08b"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 18, 9, 1, 13, DateTimeKind.Local).AddTicks(2418), new DateTime(2019, 7, 1, 18, 9, 1, 13, DateTimeKind.Local).AddTicks(2462) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ed09fe47-84c0-47b5-8007-ae2ea4350a8b"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 18, 9, 1, 13, DateTimeKind.Local).AddTicks(9120), new DateTime(2019, 7, 1, 18, 9, 1, 13, DateTimeKind.Local).AddTicks(9147) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f3c88d42-fb42-43c4-a9d4-1a738a2bd20c"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 18, 9, 1, 13, DateTimeKind.Local).AddTicks(9201), new DateTime(2019, 7, 1, 18, 9, 1, 13, DateTimeKind.Local).AddTicks(9210) });

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Teams_TeamId",
                table: "Tasks",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

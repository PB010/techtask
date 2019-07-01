using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechTask.Persistence.Migrations
{
    public partial class TaskPrioritySeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TaskPriorities",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 7, 1, 18, 9, 0, 996, DateTimeKind.Local).AddTicks(4660), "Low", new DateTime(2019, 7, 1, 18, 9, 0, 998, DateTimeKind.Local).AddTicks(1331) },
                    { 2, new DateTime(2019, 7, 1, 18, 9, 0, 998, DateTimeKind.Local).AddTicks(3652), "Normal", new DateTime(2019, 7, 1, 18, 9, 0, 998, DateTimeKind.Local).AddTicks(3665) },
                    { 3, new DateTime(2019, 7, 1, 18, 9, 0, 998, DateTimeKind.Local).AddTicks(3692), "Urgent", new DateTime(2019, 7, 1, 18, 9, 0, 998, DateTimeKind.Local).AddTicks(3696) }
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 17, 56, 25, 174, DateTimeKind.Local).AddTicks(6688), new DateTime(2019, 7, 1, 17, 56, 25, 176, DateTimeKind.Local).AddTicks(3833) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 17, 56, 25, 176, DateTimeKind.Local).AddTicks(6171), new DateTime(2019, 7, 1, 17, 56, 25, 176, DateTimeKind.Local).AddTicks(6184) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("76aebd31-0235-4ef3-a123-08d6fbc1bdcd"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 17, 56, 25, 181, DateTimeKind.Local).AddTicks(7534), new DateTime(2019, 7, 1, 17, 56, 25, 181, DateTimeKind.Local).AddTicks(7548) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bfd23b66-1a4e-41de-4aa7-08d6fae3b08b"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 17, 56, 25, 181, DateTimeKind.Local).AddTicks(5227), new DateTime(2019, 7, 1, 17, 56, 25, 181, DateTimeKind.Local).AddTicks(5254) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ed09fe47-84c0-47b5-8007-ae2ea4350a8b"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 17, 56, 25, 181, DateTimeKind.Local).AddTicks(9462), new DateTime(2019, 7, 1, 17, 56, 25, 181, DateTimeKind.Local).AddTicks(9476) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f3c88d42-fb42-43c4-a9d4-1a738a2bd20c"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 17, 56, 25, 181, DateTimeKind.Local).AddTicks(9516), new DateTime(2019, 7, 1, 17, 56, 25, 181, DateTimeKind.Local).AddTicks(9520) });
        }
    }
}

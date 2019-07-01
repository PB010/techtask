using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechTask.Persistence.Migrations
{
    public partial class ChangedShadowPropertiesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 16, 22, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 1, 16, 22, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 16, 22, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 1, 16, 22, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 16, 22, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 1, 16, 22, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 16, 22, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 1, 16, 22, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 16, 22, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 1, 16, 22, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("76aebd31-0235-4ef3-a123-08d6fbc1bdcd"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 16, 22, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 1, 16, 22, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bfd23b66-1a4e-41de-4aa7-08d6fae3b08b"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 16, 22, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 1, 16, 22, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ed09fe47-84c0-47b5-8007-ae2ea4350a8b"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 16, 22, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 1, 16, 22, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f3c88d42-fb42-43c4-a9d4-1a738a2bd20c"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2019, 7, 1, 16, 22, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 1, 16, 22, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}

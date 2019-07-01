using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechTask.Persistence.Migrations
{
    public partial class NameChangeInTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstimatedTimeToFinish",
                table: "Tasks",
                newName: "EstimatedTimeToFinishInHours");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstimatedTimeToFinishInHours",
                table: "Tasks",
                newName: "EstimatedTimeToFinish");

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
    }
}

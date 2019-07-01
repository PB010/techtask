using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechTask.Persistence.Migrations
{
    public partial class DbSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "CreatedAt", "HoursOfWorkOnAllTasks", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 7, 1, 16, 21, 25, 184, DateTimeKind.Local).AddTicks(625), 0, "Alpha", new DateTime(2019, 7, 1, 16, 21, 25, 185, DateTimeKind.Local).AddTicks(6079) },
                    { 2, new DateTime(2019, 7, 1, 16, 21, 25, 185, DateTimeKind.Local).AddTicks(8056), 0, "Beta", new DateTime(2019, 7, 1, 16, 21, 25, 185, DateTimeKind.Local).AddTicks(8074) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "Email", "FirstName", "LastName", "Password", "Role", "TeamId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("76aebd31-0235-4ef3-a123-08d6fbc1bdcd"), new DateTime(2019, 7, 1, 16, 21, 25, 191, DateTimeKind.Local).AddTicks(1716), new DateTime(1993, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.s@tech.com", "John", "Smith", "John123", 1, null, new DateTime(2019, 7, 1, 16, 21, 25, 191, DateTimeKind.Local).AddTicks(1734) },
                    { new Guid("f3c88d42-fb42-43c4-a9d4-1a738a2bd20c"), new DateTime(2019, 7, 1, 16, 21, 25, 191, DateTimeKind.Local).AddTicks(3957), new DateTime(1988, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "anthony.r@tech.com", "Anthony", "Russell", "Anthony123", 1, null, new DateTime(2019, 7, 1, 16, 21, 25, 191, DateTimeKind.Local).AddTicks(3961) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "Email", "FirstName", "LastName", "Password", "Role", "TeamId", "UpdatedAt" },
                values: new object[] { new Guid("bfd23b66-1a4e-41de-4aa7-08d6fae3b08b"), new DateTime(2019, 7, 1, 16, 21, 25, 190, DateTimeKind.Local).AddTicks(9165), new DateTime(1984, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "will.s@tech.com", "Will", "Stevens", "Will123", 0, 1, new DateTime(2019, 7, 1, 16, 21, 25, 190, DateTimeKind.Local).AddTicks(9200) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "Email", "FirstName", "LastName", "Password", "Role", "TeamId", "UpdatedAt" },
                values: new object[] { new Guid("ed09fe47-84c0-47b5-8007-ae2ea4350a8b"), new DateTime(2019, 7, 1, 16, 21, 25, 191, DateTimeKind.Local).AddTicks(3907), new DateTime(1973, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.w@tech.com", "Jane", "Williams", "Jane123", 0, 2, new DateTime(2019, 7, 1, 16, 21, 25, 191, DateTimeKind.Local).AddTicks(3921) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("76aebd31-0235-4ef3-a123-08d6fbc1bdcd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bfd23b66-1a4e-41de-4aa7-08d6fae3b08b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ed09fe47-84c0-47b5-8007-ae2ea4350a8b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f3c88d42-fb42-43c4-a9d4-1a738a2bd20c"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

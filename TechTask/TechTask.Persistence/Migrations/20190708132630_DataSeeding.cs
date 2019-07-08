using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechTask.Persistence.Migrations
{
    public partial class DataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "AdminApprovalOfTaskCompletion", "Balance", "Description", "EstimatedTimeToFinishInHours", "Name", "Status", "TaskPriorityId", "TeamId", "TotalHoursOfWork", "TrackerFirstName", "TrackerId", "TrackerLastName", "UserId" },
                values: new object[] { 1, 0, 3, "Weekly maintenance", 4, "Maintenance", 1, 2, 1, 1, "Will", new Guid("bfd23b66-1a4e-41de-4aa7-08d6fae3b08b"), "Stevens", new Guid("76aebd31-0235-4ef3-a123-08d6fbc1bdcd") });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "HoursOfWorkOnAllTasks",
                value: 1);

            migrationBuilder.InsertData(
                table: "UpdateLogs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Added new User", null, "bfd23b66-1a4e-41de-4aa7-08d6fae3b08b" },
                    { 2, new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Added new User", null, "76aebd31-0235-4ef3-a123-08d6fbc1bdcd" },
                    { 3, new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Added new User", null, "ed09fe47-84c0-47b5-8007-ae2ea4350a8b" },
                    { 4, new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Added new User", null, "f3c88d42-fb42-43c4-a9d4-1a738a2bd20c" },
                    { 5, new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Added new team", null, "1" },
                    { 6, new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Added new team", null, "2" },
                    { 7, new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Added new task", null, "1" },
                    { 8, new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Added new log to task", null, "1" },
                    { 9, new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Added new comment", null, "1" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Description", "TasksId", "UserId" },
                values: new object[] { 1, "I'm pleased with your work for now.", 1, new Guid("bfd23b66-1a4e-41de-4aa7-08d6fae3b08b") });

            migrationBuilder.InsertData(
                table: "LoggedActivities",
                columns: new[] { "Id", "CreatedAt", "Description", "HoursSpent", "TasksId", "UserId" },
                values: new object[] { 1, new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Started working on it.", 1, 1, new Guid("76aebd31-0235-4ef3-a123-08d6fbc1bdcd") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LoggedActivities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UpdateLogs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UpdateLogs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UpdateLogs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UpdateLogs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UpdateLogs",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UpdateLogs",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UpdateLogs",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UpdateLogs",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "UpdateLogs",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "HoursOfWorkOnAllTasks",
                value: 0);
        }
    }
}

using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TechTask.Application.DbLogs.Models;
using TechTask.Infrastructure.Services;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Logs;
using Xunit;

namespace TechTask.Test.DbLogServiceTest
{
    public class GetAllLogsAsyncTest
    {
        [Fact]
        public async void GetAllLogsAsync_NameQueryParameterInserted_ReturnsSpecifiedEntries()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"TechTaskTestDatabase {Guid.NewGuid()}")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.UpdateLogs.Add(new UpdateLog
                {
                    CreatedAt = DateTime.Now, Name = "Added new User", UpdatedAt = null,
                    Value = "76bbdcdc-dd26-4b6e-b7c8-08d702c8f782"
                });

                context.UpdateLogs.Add(new UpdateLog
                {
                    CreatedAt = DateTime.Now, Name = "Added new team", UpdatedAt = null,
                    Value = "5"
                });

                context.UpdateLogs.Add(new UpdateLog
                {
                    CreatedAt = DateTime.Parse("06 jul 19"), Name = "Updated Task", UpdatedAt = DateTime.Now,
                    Value = "12"
                });

                context.UpdateLogs.Add(new UpdateLog
                {
                    CreatedAt = DateTime.Parse("07 jul 19"), Name = "Removed User from Task", UpdatedAt = DateTime.Now,
                    Value = "12"
                });

                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var dbLogService = new DbLogService(context);

                //Act
                var logs = await dbLogService.GetAllLogsAsync(new DbLogQueryParameters { Name = "new" });

                //Assert
                logs.Count().Should().Be(2);
            }
        }

        [Fact]
        public async void GetAllLogsAsync_CreatedAtQueryParameterInserted_ReturnsSpecifiedEntries()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"TechTaskTestDatabase {Guid.NewGuid()}")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.UpdateLogs.Add(new UpdateLog
                {
                    CreatedAt = DateTime.Now,
                    Name = "Added new User",
                    UpdatedAt = null,
                    Value = "76bbdcdc-dd26-4b6e-b7c8-08d702c8f782"
                });

                context.UpdateLogs.Add(new UpdateLog
                {
                    CreatedAt = DateTime.Now,
                    Name = "Added new team",
                    UpdatedAt = null,
                    Value = "5"
                });

                context.UpdateLogs.Add(new UpdateLog
                {
                    CreatedAt = DateTime.Parse("06 jul 19"),
                    Name = "Updated Task",
                    UpdatedAt = DateTime.Now,
                    Value = "12"
                });

                context.UpdateLogs.Add(new UpdateLog
                {
                    CreatedAt = DateTime.Parse("07 jul 19"),
                    Name = "Removed User from Task",
                    UpdatedAt = DateTime.Now,
                    Value = "12"
                });

                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var dbLogService = new DbLogService(context);

                //Act
                var logs = await dbLogService.GetAllLogsAsync(new DbLogQueryParameters { CreatedAt = "06 jul 19"});

                //Assert
                logs.Count().Should().Be(1);
            }
        }

        [Fact]
        public async void GetAllLogsAsync_InsertedBothParameters_ReturnsSpecifiedEntries()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()   
                .UseInMemoryDatabase($"TechTaskTestDatabase {Guid.NewGuid()}")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.UpdateLogs.Add(new UpdateLog
                {
                    CreatedAt = DateTime.Now,
                    Name = "Added new User",
                    UpdatedAt = null,
                    Value = "76bbdcdc-dd26-4b6e-b7c8-08d702c8f782"
                });

                context.UpdateLogs.Add(new UpdateLog
                {
                    CreatedAt = DateTime.Now,
                    Name = "Added new team",
                    UpdatedAt = null,
                    Value = "5"
                });

                context.UpdateLogs.Add(new UpdateLog
                {
                    CreatedAt = DateTime.Parse("06 jul 19"),
                    Name = "Updated Task",
                    UpdatedAt = DateTime.Now,
                    Value = "12"
                });

                context.UpdateLogs.Add(new UpdateLog
                {
                    CreatedAt = DateTime.Parse("07 jul 19"),
                    Name = "Removed User from Task",
                    UpdatedAt = DateTime.Now,
                    Value = "12"
                });

                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var dbLogService = new DbLogService(context);

                //Act
                var logs = await dbLogService.GetAllLogsAsync(new DbLogQueryParameters
                {
                    Name = "new", 
                    CreatedAt = "08"
                });

                //Assert
                logs.Count().Should().Be(2);
            }
        }
    }
}

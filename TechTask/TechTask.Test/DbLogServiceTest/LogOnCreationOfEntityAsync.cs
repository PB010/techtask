using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using TechTask.Infrastructure.Services;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Users;
using TechTask.Persistence.Models.Users.Enums;
using Xunit;

namespace TechTask.Test.DbLogServiceTest
{
    public class LogOnCreationOfEntityAsync
    {
        [Fact]
        public async void LogOnCreationOfEntityAsync_InsertUserEntity_ShouldReturnLogForUser()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"TechTaskTestDatabase {Guid.NewGuid()}")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var userToAdd = new User
                {
                    Id = Guid.Parse("F808DF8A-3BAA-4BD8-83CA-E03E6FCE0B20"),
                    Email = "John123@tech.com",
                    FirstName = "John",
                    LastName = "Smith",
                    DateOfBirth = DateTime.Parse("03 jun 87"),
                    Password = "John123",
                    Role = Roles.User,
                    TeamId = 1
                };

                context.Users.Add(userToAdd);

                await context.SaveChangesAsync();

                var dbLog = new DbLogService(context);
                await dbLog.LogOnCreationOfEntityAsync(userToAdd);
            }

            using (var context = new AppDbContext(options))
            {
                var logFromDb = await context.UpdateLogs
                    .SingleAsync(u => u.Value == ("F808DF8A-3BAA-4BD8-83CA-E03E6FCE0B20").ToLowerInvariant());

                logFromDb.Name.Should().Be("Added new User");
            }
        }

        [Fact]
        public async void LogOnCreationOfEntityAsync_InsertTeamEntity_ShouldReturnLogForTeam()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"TechTaskTestDatabase {Guid.NewGuid()}")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var teamToAdd = new Team
                {
                    HoursOfWorkOnAllTasks = 0,
                    Id = 1,
                    Name = "Check"
                };

                context.Teams.Add(teamToAdd);

                await context.SaveChangesAsync();

                var dbLog = new DbLogService(context);
                await dbLog.LogOnCreationOfEntityAsync(teamToAdd);
            }

            using (var context = new AppDbContext(options))
            {
                var logFromDb = await context.UpdateLogs
                    .SingleAsync(u => u.Value == "1");

                logFromDb.Name.Should().Be("Added new team");
            }
        }

        [Fact]
        public async void LogOnCreationOfEntityAsync_InsertTaskEntity_ShouldReturnLogForTask()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"TechTaskTestDatabase {Guid.NewGuid()}")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var taskToAdd = new Tasks
                {
                    AdminApprovalOfTaskCompletion = 0,
                    Balance = 0,
                    Description = "Test",
                    Name = "Test name",
                    EstimatedTimeToFinishInHours = 3,
                    Id = 1,
                    Status = 0,
                    TaskPriorityId = 2,
                    TotalHoursOfWork = 0
                };

                context.Tasks.Add(taskToAdd);

                await context.SaveChangesAsync();

                var dbLog = new DbLogService(context);
                await dbLog.LogOnCreationOfEntityAsync(taskToAdd);
            }

            using (var context = new AppDbContext(options))
            {
                var logFromDb = await context.UpdateLogs
                    .SingleAsync(u => u.Value == "1");

                logFromDb.Name.Should().Be("Added new task");
            }
        }

        [Fact]
        public async void LogOnCreationOfEntityAsync_InsertCommentEntity_ShouldReturnLogForComment()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"TechTaskTestDatabase {Guid.NewGuid()}")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var commentToAdd = new Comment
                {
                    Id = 3,
                    Description = "test",
                    TasksId = 1,
                    UserId = Guid.NewGuid()
                };

                context.Comments.Add(commentToAdd);

                await context.SaveChangesAsync();

                var dbLog = new DbLogService(context);
                await dbLog.LogOnCreationOfEntityAsync(commentToAdd);
            }

            using (var context = new AppDbContext(options))
            {
                var logFromDb = await context.UpdateLogs
                    .SingleAsync(u => u.Value == "3");

                logFromDb.Name.Should().Be("Added new comment");
            }
        }

        [Fact]
        public async void LogOnCreationOfEntityAsync_InsertLogEntity_ShouldReturnLogForActivity()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"TechTaskTestDatabase {Guid.NewGuid()}")
                .Options;       
                
            using (var context = new AppDbContext(options))
            {
                var logToAdd = new LoggedActivity
                {
                    Id = 10,
                    CreatedAt = DateTime.Now,
                    Description = "Description",
                    HoursSpent = 3,
                    TasksId = 2,
                    UserId = Guid.NewGuid() 
                };

                context.LoggedActivities.Add(logToAdd);

                await context.SaveChangesAsync();

                var dbLog = new DbLogService(context);
                await dbLog.LogOnCreationOfEntityAsync(logToAdd);
            }

            using (var context = new AppDbContext(options))
            {
                var logFromDb = await context.UpdateLogs
                    .SingleAsync(u => u.Value == "10");

                logFromDb.Name.Should().Be("Added new log to task");
            }
        }
    }
}

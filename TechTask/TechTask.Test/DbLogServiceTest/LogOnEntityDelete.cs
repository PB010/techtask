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
    public class LogOnEntityDelete
    {
        [Fact]
        public async void LogOnDeleteEntity_RemoveUserFromTeam_ShouldReturnLogForUser()
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

                userToAdd.TeamId = null;
                dbLog.LogOnEntityDelete(userToAdd);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var logFromDb = await context.UpdateLogs
                    .SingleAsync(u => u.Name == "Removed User from Team");

                logFromDb.Value.Should().Be(("F808DF8A-3BAA-4BD8-83CA-E03E6FCE0B20").ToLowerInvariant());
            }
        }

        [Fact]
        public async void LogOnDeleteEntity_RemoveUserFromTask_ShouldReturnLogForTask()
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
                    Id = 11,
                    Status = 0,
                    TaskPriorityId = 2,
                    TotalHoursOfWork = 0,
                    UserId = Guid.NewGuid()
                };

                context.Tasks.Add(taskToAdd);

                await context.SaveChangesAsync();

                var dbLog = new DbLogService(context);
                await dbLog.LogOnCreationOfEntityAsync(taskToAdd);

                taskToAdd.UserId = null;

                dbLog.LogOnEntityDelete(taskToAdd);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var logFromDb = await context.UpdateLogs
                    .SingleAsync(u => u.Name == "Removed User from Task");

                logFromDb.Value.Should().Be("11");
            }
        }

        [Fact]
        public async void LogOnUpdateOfEntity_UpdateComment_ShouldReturnUpdatedCommentLog()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"TechTaskTestDatabase {Guid.NewGuid()}")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var commentToAdd = new Comment
                {
                    Id = 3,
                    Description = "Description",
                    TasksId = 3,
                    UserId = Guid.NewGuid()
                };

                context.Comments.Add(commentToAdd);

                await context.SaveChangesAsync();

                var dbLog = new DbLogService(context);
                await dbLog.LogOnCreationOfEntityAsync(commentToAdd);

                context.Remove(commentToAdd);

                dbLog.LogOnEntityDelete(commentToAdd);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var logFromDb = await context.UpdateLogs
                    .SingleAsync(u => u.Name == "Deleted Comment");

                logFromDb.Value.Should().Be("3");
            }
        }
    }
}

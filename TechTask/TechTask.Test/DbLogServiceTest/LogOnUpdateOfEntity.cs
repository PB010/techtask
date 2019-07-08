using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using TechTask.Application.Comments.Mapping;
using TechTask.Application.TeamTasks.Models;
using TechTask.Application.Users.Models;
using TechTask.Infrastructure.Services;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Users;
using TechTask.Persistence.Models.Users.Enums;
using Xunit;

namespace TechTask.Test.DbLogServiceTest
{
    public class LogOnUpdateOfEntity
    {   
        [Fact]
        public async void LogOnUpdateOfEntity_UpdateUser_ShouldReturnUpdatedUserLog()
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

                var userDto = new UserForUpdateDto
                {
                    Role = Roles.Admin,
                    TeamId = 2
                };

                userToAdd.Role = userDto.Role ?? userToAdd.Role;
                userToAdd.TeamId = userDto.TeamId;
                dbLog.LogOnUpdateOfAnEntity(userToAdd);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var logFromDb = await context.UpdateLogs
                    .SingleAsync(u => u.Name == "Updated User");

                logFromDb.Value.Should().Be(("F808DF8A-3BAA-4BD8-83CA-E03E6FCE0B20").ToLowerInvariant());
            }
        }
            
        [Fact]
        public async void LogOnUpdateOfEntity_UpdateTeam_ShouldReturnUpdatedTeamLog()
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

                teamToAdd.Name = "New name";
                dbLog.LogOnUpdateOfAnEntity(teamToAdd);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var logFromDb = await context.UpdateLogs
                    .SingleAsync(u => u.Name == "Updated Team");

                logFromDb.Value.Should().Be(("1").ToLowerInvariant());
            }
        }

        [Fact]  
        public async void LogOnUpdateOfEntity_UpdateTask_ShouldReturnUpdatedTaskLog()
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
                    TotalHoursOfWork = 0
                };

                context.Tasks.Add(taskToAdd);

                await context.SaveChangesAsync();

                var dbLog = new DbLogService(context);
                await dbLog.LogOnCreationOfEntityAsync(taskToAdd);

                var dto = new TaskForUpdateDto
                {
                    AdminApprovalOfTaskCompletion = 0,
                    Balance = 0,
                    Description = "Test",
                    Name = "Test name",
                    EstimatedTimeToFinishInHours = 3,
                    Status = 0,
                    TaskPriorityId = 2
                };
                dbLog.LogOnUpdateOfAnEntity(taskToAdd);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var logFromDb = await context.UpdateLogs
                    .SingleAsync(u => u.Name == "Updated Task");

                logFromDb.Value.Should().Be(("11").ToLowerInvariant());
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

                var dto = new CommentForUpdateDto
                {
                    Description = "New description"
                };

                dbLog.LogOnUpdateOfAnEntity(commentToAdd);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var logFromDb = await context.UpdateLogs
                    .SingleAsync(u => u.Name == "Updated Comment");

                logFromDb.Value.Should().Be(("3").ToLowerInvariant());
            }
        }
    }
}

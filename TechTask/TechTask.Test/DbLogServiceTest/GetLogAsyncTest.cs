using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using TechTask.Infrastructure.Services;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Logs;
using Xunit;

namespace TechTask.Test.DbLogServiceTest
{
    public class GetLogAsyncTest
    {
        [Fact]
        public async void GetLogAsync_InsertId_ReturnsTheCorrectLog()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"TechTaskTestDatabase {Guid.NewGuid()}")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.UpdateLogs.Add(new UpdateLog
                {
                    Id = 1,
                    CreatedAt = DateTime.Now,
                    Name = "Added new User",
                    UpdatedAt = null,
                    Value = "76bbdcdc-dd26-4b6e-b7c8-08d702c8f782"
                });

                context.UpdateLogs.Add(new UpdateLog
                {
                    Id = 2,
                    CreatedAt = DateTime.Now,
                    Name = "Added new team",
                    UpdatedAt = null,
                    Value = "5"
                });

                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var dbLogService = new DbLogService(context);
                var log = await dbLogService.GetLogAsync(1);

                log.Value.Should().Be("76bbdcdc-dd26-4b6e-b7c8-08d702c8f782");
            }
        }

        [Fact]
        public async void GetLogAsync_InsertUnexistingId_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"TechTaskTestDatabase {Guid.NewGuid()}")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.UpdateLogs.Add(new UpdateLog
                {
                    Id = 1,
                    CreatedAt = DateTime.Now,
                    Name = "Added new User",
                    UpdatedAt = null,
                    Value = "76bbdcdc-dd26-4b6e-b7c8-08d702c8f782"
                });

                context.UpdateLogs.Add(new UpdateLog
                {
                    Id = 2,
                    CreatedAt = DateTime.Now,
                    Name = "Added new team",
                    UpdatedAt = null,
                    Value = "5"
                });

                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var dbLogService = new DbLogService(context);

                var log = await dbLogService.GetLogAsync(3);

                log.Should().BeNull();  
            }
        }
    }
}

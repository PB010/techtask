using System;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Logs;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Users;

namespace TechTask.Infrastructure.Services
{
    public class DbLogService : IDbLogService
    {
        private readonly AppDbContext _context;

        public DbLogService(AppDbContext context)
        {
            _context = context;
        }   

        public async Task<int> LogOnCreationOfEntity(object entity)
        {
            var logToAdd = new UpdateLog {CreatedAt = DateTime.Now};

            switch (entity)
            {
                case User user:
                    logToAdd.Name = "Added new User";
                    logToAdd.Value = $"{user.Id}";
            
                    _context.UpdateLogs.Add(logToAdd);
            
                    break;
                case Team team:
                    logToAdd.Name = "Added new team";
                    logToAdd.Value = $"{team.Id}";
            
                    _context.UpdateLogs.Add(logToAdd);
            
                    break;
                case Tasks task:
                    logToAdd.Name = "Added new task";
                    logToAdd.Value = $"{task.Id}";
            
                    _context.UpdateLogs.Add(logToAdd);
            
                    break;
                case Comment comment:
                    logToAdd.Name = "Added new comment";
                    logToAdd.Value = $"{comment.Id}";
            
                    _context.UpdateLogs.Add(logToAdd);
            
                    break;
                case LoggedActivity loggedActivity:
                    logToAdd.Name = "Added new log to task";
                    logToAdd.Value = $"{loggedActivity.Id}";
            
                    _context.UpdateLogs.Add(logToAdd);
            
                    break;  
            }

            return await _context.SaveChangesAsync();
        }

        public async void LogOnUpdateOfAnEntity(object oldEntity, object newEntity)
        {
            var logToAdd = new UpdateLog();

            switch (oldEntity)
            {
                case User user:
                    var newUser = newEntity as User;

                    logToAdd.Value = $"{user.FirstName} {user.LastName}";
                    logToAdd.Name = "Updated the User";
                    logToAdd.CreatedAt = DateTime.Now;

                    _context.UpdateLogs.Add(logToAdd);
                    await _context.SaveChangesAsync();

                    break;
                case Team team:
                    logToAdd.Name = "Added new team";
                    logToAdd.CreatedAt = DateTime.Now;

                    _context.UpdateLogs.Add(logToAdd);
                    await _context.SaveChangesAsync();

                    break;
                case Tasks task:
                    logToAdd.Name = "Added new task";
                    logToAdd.CreatedAt = DateTime.Now;

                    _context.UpdateLogs.Add(logToAdd);
                    await _context.SaveChangesAsync();

                    break;
                case Comment comment:
                    logToAdd.Name = "Added new comment";
                    logToAdd.CreatedAt = DateTime.Now;

                    _context.UpdateLogs.Add(logToAdd);
                    await _context.SaveChangesAsync();

                    break;
                case LoggedActivity loggedActivity:
                    logToAdd.Name = "Added new log to task";
                    logToAdd.CreatedAt = DateTime.Now;

                    _context.UpdateLogs.Add(logToAdd);
                    await _context.SaveChangesAsync();

                    break;
            }
        }
    }
}

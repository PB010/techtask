using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTask.Application.DbLogs.Models;
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

        public async Task<IEnumerable<UpdateLog>> GetAllLogs(DbLogQueryParameters query)
        {
            var listOfLogs = await _context.UpdateLogs.ToListAsync();

            if (query.Name != null)
                listOfLogs = listOfLogs.Where(l => l.Name.ToLowerInvariant()
                    .Contains(query.Name)).ToList();


            if (query.CreatedAt != null)
                listOfLogs = listOfLogs.Where(l => l.CreatedAt.ToString("dd MMM yy")
                    .ToLowerInvariant()
                    .Contains(query.CreatedAt)).ToList();

            return listOfLogs;
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

        public void LogOnUpdateOfAnEntity(object oldEntity)
        {
            var updateLog = new UpdateLog();

            switch (oldEntity)
            {
                case User user:
                    var logForUser = _context.UpdateLogs.Single(t => t.Value == user.Id.ToString() &&
                                                                     t.Name == "Added new User");
                    updateLog.Name = "Updated User";
                    updateLog.CreatedAt = logForUser.CreatedAt;
                    updateLog.UpdatedAt = DateTime.Now;
                    updateLog.Value = logForUser.Value;

                    _context.UpdateLogs.Add(updateLog);
                    break;
                case Team team:
                    var logForTeam = _context.UpdateLogs.Single(t => t.Value == team.Id.ToString() &&
                                                                    t.Name == "Added new team");
                    updateLog.Name = "Updated Team";
                    updateLog.CreatedAt = logForTeam.CreatedAt;
                    updateLog.UpdatedAt = DateTime.Now;
                    updateLog.Value = logForTeam.Value;

                    _context.UpdateLogs.Add(updateLog);
                    break;
                case Tasks task:
                    var logForTask = _context.UpdateLogs.Single(t => t.Value == task.Id.ToString() &&
                                                                     t.Name == "Added new task");
                    updateLog.Name = "Updated Task";
                    updateLog.CreatedAt = logForTask.CreatedAt;
                    updateLog.UpdatedAt = DateTime.Now;
                    updateLog.Value = logForTask.Value;

                    _context.UpdateLogs.Add(updateLog);
                    break;
                case Comment comment:
                    var logForComment = _context.UpdateLogs.Single(t => t.Value == comment.Id.ToString() &&
                                                                     t.Name == "Added new comment");
                    updateLog.Name = "Updated Comment";
                    updateLog.CreatedAt = logForComment.CreatedAt;
                    updateLog.UpdatedAt = DateTime.Now;
                    updateLog.Value = logForComment.Value;

                    _context.UpdateLogs.Add(updateLog);
                    break;
            }
        }

        public void LogOnEntityDelete(object oldEntity)
        {
            var updateLog = new UpdateLog();

            switch (oldEntity)      
            {
                case User user:
                    var logForUser = _context.UpdateLogs.Single(t => t.Value == user.Id.ToString() &&
                                                                     t.Name == "Added new User");
                    updateLog.Name = "Removed User from Team";
                    updateLog.CreatedAt = logForUser.CreatedAt;
                    updateLog.UpdatedAt = DateTime.Now;
                    updateLog.Value = logForUser.Value;

                    _context.UpdateLogs.Add(updateLog);
                    break;
                case Tasks task:
                    var logForTask = _context.UpdateLogs.Single(t => t.Value == task.Id.ToString() &&
                                                                     t.Name == "Added new task");
                    updateLog.Name = "Removed User from Task";
                    updateLog.CreatedAt = logForTask.CreatedAt;
                    updateLog.UpdatedAt = DateTime.Now;
                    updateLog.Value = logForTask.Value;

                    _context.UpdateLogs.Add(updateLog);
                    break;
                case Comment comment:
                    var logForComment = _context.UpdateLogs.Single(t => t.Value == comment.Id.ToString() &&
                                                                     t.Name == "Added new comment");
                    updateLog.Name = "Deleted Comment";
                    updateLog.CreatedAt = logForComment.CreatedAt;
                    updateLog.UpdatedAt = DateTime.Now;
                    updateLog.Value = logForComment.Value;

                    _context.UpdateLogs.Add(updateLog);
                    break;
            }
        }
    }
}

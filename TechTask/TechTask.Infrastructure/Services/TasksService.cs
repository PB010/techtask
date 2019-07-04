using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.TeamTasks.Models;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Task;

namespace TechTask.Infrastructure.Services
{
    public class TasksService : ITasksService
    {
        private readonly AppDbContext _context;

        public TasksService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tasks> GetTaskWithEagerLoadingAsync(int id)
        {
            return await _context.Tasks.Include(t => t.Team)
                .Include(t => t.TaskPriority)
                .Include(t => t.User)
                .Include(t => t.Comments)
                .Include(t => t.Log)
                .SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tasks> GetTaskWithoutEagerLoadingAsync(int id)
        {
            return await _context.Tasks.Include(t => t.Team)
                .Include(t => t.TaskPriority)
                .SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tasks>> GetAllTasksAsync()
        {
            return await _context.Tasks.Include(t => t.Team)
                .Include(t => t.TaskPriority)
                .Include(t => t.User)
                .Include(t => t.Comments)
                .Include(t => t.Log)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tasks>> GetAllTasksForATeamAsync(int teamId)
        {
            return await _context.Tasks.Include(t => t.Team)
                .Include(t => t.TaskPriority)
                .Include(t => t.User)
                .Include(t => t.Comments)
                .Include(t => t.Log)
                .Where(t => t.TeamId == teamId)
                .ToListAsync();
        }

        public async Task<int> AddTask(Tasks task)
        {
            _context.Tasks.Add(task);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateTask(Tasks task, TaskForUpdateDto dto)
        {
            task.Name = dto.Name ?? task.Name;
            task.Description = dto.Description ?? task.Description;
            task.EstimatedTimeToFinishInHours = dto.EstimatedTimeToFinishInHours ??
                task.EstimatedTimeToFinishInHours;
            task.TaskPriorityId = dto.TaskPriorityId ?? task.TaskPriorityId;
            task.Status = dto.Status ?? task.Status;
            task.Balance = dto.Balance ?? task.Balance;
            task.AdminApprovalOfTaskCompletion = dto.AdminApprovalOfTaskCompletion ??
                                                 task.AdminApprovalOfTaskCompletion;
            task.TrackerId = dto.TrackerId ?? task.TrackerId;
            task.TrackerFirstName = dto.TrackerFirstName ?? task.TrackerFirstName;
            task.TrackerLastName = dto.TrackerLastName ?? task.TrackerLastName;
            task.UserId = dto.UserId ?? task.UserId;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddUserToTaskAsync(Tasks task, Guid userId)  
        {
            task.UserId = userId;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveUserFromTaskAsync(Tasks task)
        {
            task.UserId = null;
            return await _context.SaveChangesAsync();
        }
    }
}

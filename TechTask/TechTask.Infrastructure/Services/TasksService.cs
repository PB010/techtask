using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Logs.Models;
using TechTask.Application.TeamTasks.Models;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Task.Enums;
using TechTask.Persistence.Models.Users;
using TaskStatus = TechTask.Persistence.Models.Task.Enums.TaskStatus;

namespace TechTask.Infrastructure.Services
{
    public class TasksService : ITasksService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly IDbLogService _dbLogService;

        public TasksService(AppDbContext context, IHttpContextAccessor accessor,
            IDbLogService dbLogService)
        {
            _context = context;
            _accessor = accessor;
            _dbLogService = dbLogService;
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
            await _context.SaveChangesAsync();
            return await _dbLogService.LogOnCreationOfEntity(task);
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

        public async Task<int> CalculateNewWorkBalanceAsync(Tasks task, LoggedActivity log)
        {
            task.TotalHoursOfWork += log.HoursSpent;
                
            if (task.EstimatedTimeToFinishInHours / 4 >= task.TotalHoursOfWork)
                task.Balance = WorkBalance.Excellent;   

            if (task.EstimatedTimeToFinishInHours / 2 >= task.TotalHoursOfWork &&
                task.EstimatedTimeToFinishInHours / 4 < task.TotalHoursOfWork)
                task.Balance = WorkBalance.Good;

            if (task.EstimatedTimeToFinishInHours / 1 >= task.TotalHoursOfWork &&
                task.EstimatedTimeToFinishInHours / 2 < task.TotalHoursOfWork)
                task.Balance = WorkBalance.Average;

            if (task.EstimatedTimeToFinishInHours / 1 < task.TotalHoursOfWork)
                task.Balance = WorkBalance.Bad;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> ChangeStatusBasedOnAdminApproval(Tasks task, LogForCreationDto log)
        {
            task.Status = log.TaskStatus;

            if (_accessor.HttpContext.User.IsInRole("Admin") &&
                task.Status == TaskStatus.Done)
            {
                task.AdminApprovalOfTaskCompletion = TrackerTaskStatus.Approved;
                return await _context.SaveChangesAsync();
            }

            if (task.Status == TaskStatus.Done &&
                task.AdminApprovalOfTaskCompletion != TrackerTaskStatus.Approved)
                task.Status = TaskStatus.Pending;
                
            return await _context.SaveChangesAsync();
        }

        public async Task<int> ChangeTasksAdminApprovalState(Tasks task)
        {
            switch (_accessor.HttpContext.Request.Method)
            {
                case "POST":
                    task.AdminApprovalOfTaskCompletion = TrackerTaskStatus.Approved;
                    break;
                case "DELETE":
                    task.AdminApprovalOfTaskCompletion = TrackerTaskStatus.Denied;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (task.AdminApprovalOfTaskCompletion)
            {
                case TrackerTaskStatus.Approved:
                    task.Status = TaskStatus.Done;
                    break;
                case TrackerTaskStatus.Denied:
                    task.Status = TaskStatus.InProgress;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> ReopenTask(Tasks task)
        {
            task.Status = TaskStatus.InProgress;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddUserToTaskAsync(Tasks task, Guid userId)  
        {
            task.UserId = userId;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveUserFromTaskAsync(Tasks task, TaskStatus status)
        {
            task.UserId = null;
            task.Status = status;
            return await _context.SaveChangesAsync();
        }
    }
}

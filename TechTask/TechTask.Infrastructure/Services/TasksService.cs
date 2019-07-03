using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
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

        public async Task<Tasks> GetTaskAsync(int id, bool includeAllChildren)
        {
            if (includeAllChildren)
                return await _context.Tasks.Include(t => t.Team)
                    .Include(t => t.TaskPriority)
                    .Include(t => t.User)
                    .Include(t => t.Comments)
                    .Include(t => t.Log)
                    .SingleOrDefaultAsync(t => t.Id == id);

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
    }
}

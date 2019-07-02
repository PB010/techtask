using Microsoft.EntityFrameworkCore;
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

        public void AddTasks(Tasks task)
        {
            _context.Tasks.Add(task);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Users;

namespace TechTask.Infrastructure.Services
{
    public class LogService : ILogService
    {
        private readonly AppDbContext _context;
        private readonly IDbLogService _dbLogService;

        public LogService(AppDbContext context, IDbLogService dbLogService)
        {
            _context = context;
            _dbLogService = dbLogService;
        }

        public async Task<IEnumerable<LoggedActivity>> GetAllLogsAsync(int taskId)
        {
            return await _context.LoggedActivities.Where(l => l.TasksId == taskId)
                .ToListAsync();
        }

        public async Task<LoggedActivity> GetLogAsync(int id)
        {
            return await _context.LoggedActivities.SingleOrDefaultAsync(l => l.Id == id);
        }

        public async Task<int> AddNewLogAsync(LoggedActivity log)
        {
            _context.Add(log);
            await _context.SaveChangesAsync();

            return await _dbLogService.LogOnCreationOfEntity(log);
        }
    }
}

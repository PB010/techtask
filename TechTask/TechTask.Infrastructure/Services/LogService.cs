using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Logs.Models;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Users;

namespace TechTask.Infrastructure.Services
{
    public class LogService : ILogService
    {
        private readonly AppDbContext _context;

        public LogService(AppDbContext context)
        {
            _context = context;
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
            return await _context.SaveChangesAsync();
        }

        public void AssignDateTimeToListLogDetailsDto(List<LogDetailsDto> dto, int taskId)
        {
            var createdAt = _context.LoggedActivities.Where(l => l.TasksId == taskId)
                .Select(s => EF.Property<DateTime>(s, "CreatedAt"))
                .ToList();

            for (var i = 0; i < dto.Count; i++)
            {
                dto[i].CreatedAt = createdAt[i].ToString("dd MMM yy");
            }
        }
    }
}

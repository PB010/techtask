using System.Threading.Tasks;
using TechTask.Application.Interfaces;
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

        public async Task<int> AddNewLog(LoggedActivity log)
        {
            _context.Add(log);
            return await _context.SaveChangesAsync();
        }
    }
}

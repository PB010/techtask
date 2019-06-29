using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Users;

namespace TechTask.Infrastructure.Services
{
    public class TeamService
    {
        private readonly AppDbContext _context;

        public TeamService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Team> GetTeamAsync(int id, bool includeChild)
        {
            if (includeChild)
                return await _context.Teams.Include(t => t.Tasks)
                    .Include(t => t.Users)
                    .SingleOrDefaultAsync(t => t.Id == id);

            return await _context.Teams.SingleOrDefaultAsync(t => t.Id == id);
        }

        public void AddTeam(Team team)
        {
            _context.Teams.Add(team);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

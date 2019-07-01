using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Users;

namespace TechTask.Infrastructure.Services
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _context;

        public TeamService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Team> GetTeamAsync(int id, bool includeChild)
        {
            if (includeChild)
                return await _context.Teams.Include(t => t.Tasks)
                    .Include(t => t.Users)
                    .ThenInclude(u => u.Comments)
                    .Include(t => t.Users)
                    .ThenInclude(u => u.Log)
                    .SingleOrDefaultAsync(t => t.Id == id);

            return await _context.Teams.SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync(bool includeChild)
        {
            if (includeChild)
                return await _context.Teams.Include(t => t.Tasks)
                    .Include(t => t.Users)
                    .ThenInclude(u => u.Comments)
                    .Include(t => t.Users)
                    .ThenInclude(u => u.Log)
                    .ToListAsync();

            return await _context.Teams.ToListAsync();
        }   

        public void RemoveUserFromTeam(User user)
        {
            user.TeamId = null;
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

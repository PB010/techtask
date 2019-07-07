using Microsoft.EntityFrameworkCore;
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
        private readonly IDbLogService _dbLogService;

        public TeamService(AppDbContext context, IDbLogService dbLogService)
        {
            _context = context;
            _dbLogService = dbLogService;
        }

        public async Task<Team> GetTeamWithEagerLoadingAsync(int id)
        {
            return await _context.Teams.Include(t => t.Tasks)
                .Include(t => t.Users)
                .ThenInclude(u => u.Comments)
                .Include(t => t.Users)
                .ThenInclude(u => u.Log)
                .SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Team> GetTeamWithoutEagerLoadingAsync(int id)
        {
            return await _context.Teams.SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Team>> GetAllTeamsWithEagerLoadingAsync()
        {
            return await _context.Teams.Include(t => t.Tasks)
                .Include(t => t.Users)
                .ThenInclude(u => u.Comments)
                .Include(t => t.Users)
                .ThenInclude(u => u.Log)
                .ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetAllTeamsWithoutEagerLoadingAsync()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<int> CalculateTotalHoursOfWorkAsync(Team team, LoggedActivity log)
        {
            team.HoursOfWorkOnAllTasks += log.HoursSpent;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveUserFromTeam(User user)
        {
            user.TeamId = null;
            _dbLogService.LogOnEntityDelete(user);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> AssignUserToTeam(Team team, User user)
        {
            team.Users.Add(user);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddTeam(Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return await _dbLogService.LogOnCreationOfEntity(team);
        }

        public async void UpdateTeamName(Team team, string name)
        {
            team.Name = name;
            _dbLogService.LogOnUpdateOfAnEntity(team);

            await _context.SaveChangesAsync();
        }
    }
}

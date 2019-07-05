using System.Collections.Generic;
using System.Threading.Tasks;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Interfaces
{
    public interface ITeamService   
    {
        Task<Team> GetTeamWithEagerLoadingAsync(int id);
        Task<Team> GetTeamWithoutEagerLoadingAsync(int id);
        Task<IEnumerable<Team>> GetAllTeamsWithEagerLoadingAsync();
        Task<IEnumerable<Team>> GetAllTeamsWithoutEagerLoadingAsync();
        Task<int> CalculateTotalHoursOfWorkAsync(Team team, Tasks task);
        Task<int> RemoveUserFromTeam(User user);
        Task<int> AssignUserToTeam(Team team, User user);
        Task<int> AddTeam(Team team);
        void UpdateTeamName(Team team, string name);
    }
}
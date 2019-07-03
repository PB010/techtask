using System.Collections.Generic;
using System.Threading.Tasks;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Interfaces
{
    public interface ITeamService   
    {
        Task<Team> GetTeamAsync(int id, bool includeChild);
        Task<IEnumerable<Team>> GetAllTeamsWithEagerLoadingAsync();
        Task<IEnumerable<Team>> GetAllTeamsWithoutEagerLoadingAsync();
        Task<int> RemoveUserFromTeam(User user);
        Task<int> AssignUserToTeam(Team team, User user);
        Task<int> AddTeam(Team team);
    }
}
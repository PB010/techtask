using System.Collections.Generic;
using System.Threading.Tasks;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Interfaces
{
    public interface ITeamService
    {
        Task<Team> GetTeamAsync(int id, bool includeChild);
        Task<IEnumerable<Team>> GetAllTeamsAsync(bool includeChild);
        void RemoveUserFromTeam(User user);
        void AddTeam(Team team);
        Task<int> SaveChangesAsync();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTask.Persistence.Models.Task;

namespace TechTask.Application.Interfaces
{
    public interface ITasksService
    {
        Task<Tasks> GetTaskWithEagerLoadingAsync(int id);
        Task<Tasks> GetTaskWithoutEagerLoadingAsync(int id);
        Task<IEnumerable<Tasks>> GetAllTasksAsync();
        Task<IEnumerable<Tasks>> GetAllTasksForATeamAsync(int teamId);
        Task<int> AddTask(Tasks task);
    }
}

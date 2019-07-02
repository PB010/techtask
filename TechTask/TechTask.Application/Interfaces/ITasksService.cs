using System.Threading.Tasks;
using TechTask.Persistence.Models.Task;

namespace TechTask.Application.Interfaces
{
    public interface ITasksService
    {
        Task<Tasks> GetTaskAsync(int id, bool includeAllChildren);
        void AddTasks(Tasks task);
        Task<int> SaveChangesAsync();
    }
}

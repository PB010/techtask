using System.Collections.Generic;
using System.Threading.Tasks;
using TechTask.Persistence.Models.Task;

namespace TechTask.Application.Interfaces
{
    public interface ITasksService
    {
        Task<Tasks> GetTaskAsync(int id, bool includeAllChildren);
        Task<IEnumerable<Tasks>> GetAllTasksAsync();
        void AddTask(Tasks task);
        /// Task<int> SaveChangesAsync();
    }
}

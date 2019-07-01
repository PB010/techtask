using System.Threading.Tasks;
using TechTask.Persistence.Models.Task;

namespace TechTask.Application.Interfaces
{
    public interface ITasksService
    {
        void AddTasks(Tasks task);
        Task<int> SaveChangesAsync();
    }
}

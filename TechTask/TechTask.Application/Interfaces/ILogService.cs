using System.Threading.Tasks;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Interfaces
{
    public interface ILogService
    {
        Task<int> AddNewLog(LoggedActivity log);
    }
}
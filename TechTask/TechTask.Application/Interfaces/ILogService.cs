using System.Collections.Generic;
using System.Threading.Tasks;
using TechTask.Application.Logs.Models;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Interfaces
{
    public interface ILogService
    {
        Task<IEnumerable<LoggedActivity>> GetAllLogsAsync(int taskId);
        Task<LoggedActivity> GetLogAsync(int id);
        Task<int> AddNewLogAsync(LoggedActivity log);
        void AssignDateTimeToListLogDetailsDto(List<LogDetailsDto> dto, int taskId);
    }
}
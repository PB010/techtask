using System.Threading.Tasks;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Users;
using TaskStatus = TechTask.Persistence.Models.Task.Enums.TaskStatus;

namespace TechTask.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailWhenUsersHaveNoTasksAsync(User user);
        Task SendEmailIfStatusChangedAsync(Tasks task, TaskStatus? status);
        Task SendEmailAsync(Tasks task);
    }
}
    
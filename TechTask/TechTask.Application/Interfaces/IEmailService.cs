using System.Threading.Tasks;
using TechTask.Persistence.Models.Task;
using TaskStatus = TechTask.Persistence.Models.Task.Enums.TaskStatus;

namespace TechTask.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailWhenUsersHaveNoTasksAsync(string email, string subject, string message);
        Task SendEmailIfStatusChangedAsync(Tasks task, TaskStatus? status,
            string email, string subject, string message);
        Task SendEmailAsync(string email,
            string subject, string message);
    }
}
    
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTask.Application.Logs.Models;
using TechTask.Application.TeamTasks.Models;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Interfaces
{
    public interface ITasksService
    {
        Task<Tasks> GetTaskWithEagerLoadingAsync(int id);
        Task<Tasks> GetTaskWithoutEagerLoadingAsync(int id);
        Task<IEnumerable<Tasks>> GetAllTasksAsync();
        Task<IEnumerable<Tasks>> GetAllTasksForATeamAsync(int teamId);
        Task<int> AddTask(Tasks task);
        Task<int> UpdateTask(Tasks task, TaskForUpdateDto dto);
        Task<int> CalculateNewWorkBalanceAsync(Tasks task, LoggedActivity log);
        Task<int> ChangeStatusBasedOnAdminApproval(Tasks task, LogForCreationDto log);
        Task<int> AddUserToTaskAsync(Tasks task, Guid userId);
        Task<int> RemoveUserFromTaskAsync(Tasks task);
        void AssignDateTimeToCreatedAt(TaskDetailsDto dto);
        Task<int> ChangeTasksAdminApprovalState(Tasks task);
    }
}
    
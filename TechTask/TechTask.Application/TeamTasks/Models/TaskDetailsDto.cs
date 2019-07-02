using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.TeamTasks.Models
{
    public class TaskDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int EstimatedTimeToFinishInHours { get; set; }
        public int TotalHoursOfWork { get; set; }
        public List<Comment> Comments { get; set; }
        public List<LoggedActivity> Log { get; set; }
        public TaskPriority Priority { get; set; }
        public string Status { get; set; }
        public string Balance { get; set; }
        public string AdminApprovalOfTaskCompletion { get; set; }
        public string TeamName { get; set; }
        public string TrackerName { get; set; }
        public string UserOnTask { get; set; }

        public static Expression<Func<Tasks, TaskDetailsDto>> ProjectionNoUsers
        {
            get
            {
                return p => new TaskDetailsDto
                {
                    Id = p.Id,
                    Balance = p.Balance.ToString(),
                    Comments = p.Comments,
                    Description = p.Description,
                    EstimatedTimeToFinishInHours = p.EstimatedTimeToFinishInHours,
                    Log = p.Log,
                    Name = p.Name,
                    TeamName = p.Team.Name,
                    Priority = p.TaskPriority,
                    TotalHoursOfWork = p.TotalHoursOfWork,
                    Status = p.Status.ToString(),
                    AdminApprovalOfTaskCompletion = p.AdminApprovalOfTaskCompletion.ToString()
                };  
            }
        }

        public static TaskDetailsDto TaskDetailsWithNoUsers(Tasks task)
        {
            return ProjectionNoUsers.Compile().Invoke(task);
        }

        public static Expression<Func<Tasks, TaskDetailsDto>> ProjectionFull
        {
            get
            {
                return p => new TaskDetailsDto
                {
                    Id = p.Id,
                    Balance = p.Balance.ToString(),
                    Comments = p.Comments,
                    Description = p.Description,
                    EstimatedTimeToFinishInHours = p.EstimatedTimeToFinishInHours,
                    Log = p.Log,
                    Name = p.Name,
                    TeamName = p.Team.Name,
                    Priority = p.TaskPriority,
                    TotalHoursOfWork = p.TotalHoursOfWork,
                    Status = p.Status.ToString(),
                    AdminApprovalOfTaskCompletion = p.AdminApprovalOfTaskCompletion.ToString(),
                    UserOnTask = $"{p.User.FirstName} {p.User.LastName}",
                    TrackerName = $"{p.TrackerFirstName} {p.TrackerLastName}"
                };
            }
        }

        public static TaskDetailsDto TaskDetailsFull(Tasks task)
        {
            return ProjectionFull.Compile().Invoke(task);
        }
    }
}       

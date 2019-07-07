using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TechTask.Application.TeamTasks.Commands;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Task.Enums;

namespace TechTask.Application.Filters.Validators.TaskValidator
{
    public class ValidateAssignToUserCommand : ActionFilterAttribute
    {
        private readonly AppDbContext _appDbContext;

        public ValidateAssignToUserCommand(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var assignToTaskCommand = context.ActionArguments["command"] as AssignUserToTaskCommand;
            var teamId = context.RouteData.Values["teamId"];
            var taskId = context.RouteData.Values["taskId"];

            if (assignToTaskCommand.UserId != null)
            {
                var teamIdAsInt = int.Parse(teamId.ToString());
                var usersTeamCheck = _appDbContext.Teams
                    .Include(t => t.Users)
                    .Single(t => t.Id == teamIdAsInt).Users.Any(u => u.Id == assignToTaskCommand.UserId);

                if (!usersTeamCheck)
                {
                    context.ModelState.AddModelError("userId", "This user is not a part of this team.");
                    var httpResult = new BadRequestObjectResult(context.ModelState) { StatusCode = 400 };
                    context.Result = httpResult;
                    return;
                }

                var taskIdAsInt = int.Parse(taskId.ToString());
                var taskCompletionCheck = _appDbContext.Tasks.Single(t => t.Id == taskIdAsInt);

                if (taskCompletionCheck.Status == TaskStatus.Done ||
                    taskCompletionCheck.Status == TaskStatus.Pending)
                {
                    context.ModelState.AddModelError("userId", "You cannot assign someone to a finished task.");
                    var httpResult = new BadRequestObjectResult(context.ModelState) { StatusCode = 400 };
                    context.Result = httpResult;
                    return;
                }
            }

            base.OnActionExecuting(context);
        }
    }
}

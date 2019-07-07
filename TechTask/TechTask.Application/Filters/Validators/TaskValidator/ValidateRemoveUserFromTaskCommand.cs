using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TechTask.Application.TeamTasks.Models;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Task.Enums;

namespace TechTask.Application.Filters.Validators.TaskValidator
{
    public class ValidateRemoveUserFromTaskCommand : ActionFilterAttribute
    {
        private readonly AppDbContext _appDbContext;

        public ValidateRemoveUserFromTaskCommand(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var dtoForRemoval = context.ActionArguments["dto"] as TaskForRemovalDto;
            var taskId = context.RouteData.Values["taskId"];

            if (dtoForRemoval.UserId != null)
            {
                var taskIdAsInt = int.Parse(taskId.ToString());
                var taskCheck = _appDbContext.Tasks
                    .Include(t => t.User)
                    .Single(t => t.Id == taskIdAsInt).UserId.HasValue;

                if (!taskCheck)
                {
                    context.ModelState.AddModelError("userId", "This task doesn't have anyone assigned to it.");
                    var httpResult = new BadRequestObjectResult(context.ModelState) { StatusCode = 400 };
                    context.Result = httpResult;
                    return;
                }

                var taskCompletionCheck = _appDbContext.Tasks.Single(t => t.Id == taskIdAsInt);

                if (taskCompletionCheck.Status == TaskStatus.Done ||
                    taskCompletionCheck.Status == TaskStatus.Pending)
                {
                    context.ModelState.AddModelError("userId", "You cannot remove someone from a finished task.");
                    var httpResult = new BadRequestObjectResult(context.ModelState) { StatusCode = 400 };
                    context.Result = httpResult;
                    return;
                }
            }
            base.OnActionExecuting(context);
        }
    }
}

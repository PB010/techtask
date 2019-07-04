using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TechTask.Application.TeamTasks.Commands;
using TechTask.Persistence.Context;

namespace TechTask.Application.Filters.TaskValidator
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
            var assignToTaskCommand = context.ActionArguments["command"] as RemoveUserFromTaskCommand;
            var taskId = context.RouteData.Values["taskId"];

            if (assignToTaskCommand.UserId != null)
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
            }
            base.OnActionExecuting(context);
        }
    }
}

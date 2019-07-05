using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Task.Enums;

namespace TechTask.Application.Filters.TaskValidator
{
    public class ValidateApproveTaskCompletion : ActionFilterAttribute
    {
        private readonly AppDbContext _appDbContext;

        public ValidateApproveTaskCompletion(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var taskId = context.RouteData.Values["taskId"];

            if (taskId != null)
            {
                var taskIdAsInt = int.Parse(taskId.ToString());
                var taskIdCheck = _appDbContext.Tasks.Single(t => t.Id == taskIdAsInt);

                if (taskIdCheck.Status != TaskStatus.Pending)
                {
                    context.ModelState.AddModelError("status", "You can only approve the completion of a pending task.");
                    var httpResult = new BadRequestObjectResult(context.ModelState) { StatusCode = 400 };
                    context.Result = httpResult;
                    return;
                }
            }

            base.OnActionExecuting(context);
        }
    }
}

using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TechTask.Persistence.Context;

namespace TechTask.Application.Filters.Validator
{
    public class ValidateRouteAttributes : ActionFilterAttribute
    {
        private readonly AppDbContext _context;

        public ValidateRouteAttributes(AppDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.RouteData.Values["userId"];
            var teamId = context.RouteData.Values["teamId"];
            var taskId = context.RouteData.Values["taskId"];

            if (userId != null)
            {
                var userIdAsGuid = new Guid($"{userId}");
                var userIdCheck = _context.Users.Any(u => u.Id == userIdAsGuid);

                if (!userIdCheck)
                {
                    context.ModelState.AddModelError("userId", "This user doesn't exist");
                    var httpResult = new BadRequestObjectResult(context.ModelState) { StatusCode = 404 };
                    context.Result = httpResult;
                    return;
                }
            }

            if (teamId != null)
            {
                var teamIdAsInt = (int) teamId;
                var teamIdCheck = _context.Teams.Any(t => t.Id == teamIdAsInt);

                if (!teamIdCheck)
                {
                    context.ModelState.AddModelError("teamId", "This team doesn't exist.");
                    var httpResult = new BadRequestObjectResult(context.ModelState) { StatusCode = 404 };
                    context.Result = httpResult;
                    return;
                }

            }

            if (taskId != null)
            {
                var taskIdAsInt = (int) taskId;
                var taskIdCheck = _context.Tasks.Any(t => t.Id == taskIdAsInt);

                if (!taskIdCheck)
                {
                    context.ModelState.AddModelError("taskId", "This task doesn't exist.");
                    var httpResult = new BadRequestObjectResult(context.ModelState) { StatusCode = 404 };
                    context.Result = httpResult;
                    return;
                }
            }
            

            base.OnActionExecuting(context);
        }
    }
}

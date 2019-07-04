using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TechTask.Application.TeamTasks.Models;
using TechTask.Persistence.Context;

namespace TechTask.Application.Filters.TaskValidator
{
    public class ValidateTaskForCreationDto : ActionFilterAttribute
    {
        private readonly AppDbContext _appDbContext;

        public ValidateTaskForCreationDto(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var taskDto = context.ActionArguments["dto"] as TaskForCreationDto;
            var teamId = context.RouteData.Values["teamId"];

            if (taskDto.UserId != null)
            {   
                var teamIdAsInt = int.Parse(teamId.ToString());
                var usersTeamCheck = _appDbContext.Teams
                    .Include(t => t.Users)
                    .Single(t => t.Id == teamIdAsInt).Users.Any(u => u.Id == taskDto.UserId);

                if (!usersTeamCheck)
                {
                    context.ModelState.AddModelError("userId", "This user is not a part of this team.");
                    var httpResult = new BadRequestObjectResult(context.ModelState) { StatusCode = 404 };
                    context.Result = httpResult;
                    return;
                }
            }

            base.OnActionExecuting(context);
        }
    }
}

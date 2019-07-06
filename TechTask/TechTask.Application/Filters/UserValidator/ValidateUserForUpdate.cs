using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using TechTask.Application.Users.Models;
using TechTask.Persistence.Context;

namespace TechTask.Application.Filters.UserValidator
{
    public class ValidateUserForUpdate : ActionFilterAttribute
    {
        private readonly AppDbContext _appDbContext;

        public ValidateUserForUpdate(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var teamId = context.ActionArguments["dto"] as UserForUpdateDto;

            if (teamId?.TeamId != null)
            {
                var teamFromDb = _appDbContext.Teams.Any(t => t.Id == teamId.TeamId);

                if (!teamFromDb)
                {
                    context.ModelState.AddModelError("teamId", "This team doesn't exist");
                    var httpResult = new BadRequestObjectResult(context.ModelState) { StatusCode = 404 };
                    context.Result = httpResult;
                    return;
                }
            }

            base.OnActionExecuting(context);
        }
    }
}

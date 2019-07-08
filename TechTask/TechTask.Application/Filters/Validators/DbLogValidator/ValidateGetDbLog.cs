using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using TechTask.Persistence.Context;

namespace TechTask.Application.Filters.Validators.DbLogValidator
{
    public class ValidateGetDbLog : ActionFilterAttribute
    {
        private readonly AppDbContext _appDbContext;

        public ValidateGetDbLog(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var logId = context.RouteData.Values["logId"];

            if (logId != null)
            {
                var logIdToInt = int.Parse(logId.ToString());
                var dbCheck = _appDbContext.UpdateLogs.Any(t => t.Id == logIdToInt);

                if (!dbCheck)
                {
                    context.ModelState.AddModelError("logId", "There is no log with this id.");
                    var httpResult = new BadRequestObjectResult(context.ModelState) { StatusCode = 404 };
                    context.Result = httpResult;
                    return;
                }
            }

            base.OnActionExecuting(context);
        }
    }
}

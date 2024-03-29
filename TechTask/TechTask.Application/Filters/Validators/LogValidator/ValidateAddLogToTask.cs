﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Task.Enums;

namespace TechTask.Application.Filters.Validators.LogValidator
{
    public class ValidateAddLogToTask : ActionFilterAttribute
    {
        private readonly AppDbContext _appDbContext;

        public ValidateAddLogToTask(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var taskId = context.RouteData.Values["taskId"];

            if (taskId != null)
            {
                var taskIdAsInt = int.Parse(taskId.ToString());
                var taskCheck = _appDbContext.Tasks
                    .Single(t => t.Id == taskIdAsInt);

                if (taskCheck.Status == TaskStatus.Done ||
                    taskCheck.Status == TaskStatus.Pending ||
                    taskCheck.Status == TaskStatus.Canceled)
                {
                    context.ModelState.AddModelError("taskId", "You cannot do logging on a finished/cancelled task.");
                    var httpResult = new BadRequestObjectResult(context.ModelState) { StatusCode = 400 };
                    context.Result = httpResult;
                    return;
                }
            }

            base.OnActionExecuting(context);
        }
    }
}

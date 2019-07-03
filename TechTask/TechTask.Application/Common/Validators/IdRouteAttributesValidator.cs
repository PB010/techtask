using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;

namespace TechTask.Application.Common.Validators
{
    //public class IdRouteAttributesValidator : AbstractValidator<int>
    //{
    //    public IdRouteAttributesValidator(AppDbContext context)
    //    {
    //        RuleFor(x => x).Must(m => context.Teams.Any(t => t.UserId == m))
    //            .WithMessage("This team was not found.")
    //            .WithErrorCode("404");
    //    }
    //}
    public class Test
    {
        public static Guid IdFromRouteValue(IActionContextAccessor accessor)
        {
            var id = accessor.ActionContext.RouteData.Values["id"];
            return new Guid($"{id}");
        }
    }
}

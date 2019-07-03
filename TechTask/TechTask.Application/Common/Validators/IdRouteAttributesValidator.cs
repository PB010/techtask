using FluentValidation;
using System.Linq;
using TechTask.Persistence.Context;

namespace TechTask.Application.Common.Validators
{
    public class IdRouteAttributesValidator : AbstractValidator<int>
    {
        public IdRouteAttributesValidator(AppDbContext context)
        {
            RuleFor(x => x).Must(m => context.Teams.Any(t => t.Id == m))
                .WithMessage("This team was not found.")
                .WithErrorCode("404");
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TechTask.Application.Interfaces;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Task.Enums;
using TechTask.Persistence.Models.Users.Enums;

namespace TechTask.Application.Filters.Email
{
    public class EmailSenderService : ActionFilterAttribute
    {
        private readonly IEmailService _emailService;
        private readonly AppDbContext _context;

        public EmailSenderService(IEmailService emailService, AppDbContext context)
        {
            _emailService = emailService;
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var usersFromDb =  _context.Users.Include(u => u.Tasks)
                .Where(u => u.Role == Roles.User).ToList();

            var usersWithNoTasksInProgress = usersFromDb.Where(u => u.Tasks
                .All(t => t.Status != TaskStatus.Assigned &&
                          t.Status != TaskStatus.InProgress)).ToList();

            foreach (var user in usersWithNoTasksInProgress)
            {
                _emailService.SendEmailAsync(
                    "test@tech.com",
                    $"User has no task assigned to him.",
                    $"{user.FirstName} {user.LastName} has no task assigned to him, please make sure he gets one.");
            }

            base.OnActionExecuting(context);
        }
    }
}

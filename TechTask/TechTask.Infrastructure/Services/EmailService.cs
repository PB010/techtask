using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Users;
using TaskStatus = TechTask.Persistence.Models.Task.Enums.TaskStatus;

namespace TechTask.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public EmailService(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task SendEmailWhenUsersHaveNoTasksAsync(User user)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = _configuration["Email:Email"],
                    Password = _configuration["Email:Password"]
                };

                client.Credentials = credential;
                client.Host = _configuration["Email:Host"];
                client.Port = int.Parse(_configuration["Email:Port"]);
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress("admin@tech.com"));
                    emailMessage.From = new MailAddress(_configuration["Email:Email"]);
                    emailMessage.Subject = "User has no task assigned to him.";
                    emailMessage.Body = $"{user.FirstName} {user.LastName} has no task assigned to him, please make sure he gets one.";
                    client.Send(emailMessage);
                }
            }

            await Task.CompletedTask;
        }

        public async Task SendEmailIfStatusChangedAsync(Tasks task, TaskStatus? status)
        {
            if (status != null && status != task.Status)
            {
                using (var client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = _configuration["Email:Email"],
                        Password = _configuration["Email:Password"]
                    };

                    client.Credentials = credential;
                    client.Host = _configuration["Email:Host"];
                    client.Port = int.Parse(_configuration["Email:Port"]);
                    client.EnableSsl = true;

                    using (var emailMessage = new MailMessage())
                    {
                        emailMessage.To.Add(new MailAddress("admin@tech.com"));
                        emailMessage.From = new MailAddress(_configuration["Email:Email"]);
                        emailMessage.Subject = "Status change";
                        emailMessage.Body = $"Status for task '{task.Name}' has changed to {task.Status.ToString()}";
                        client.Send(emailMessage);
                    }
                }

                await Task.CompletedTask;
            }
        }

        public async Task SendEmailAsync(Tasks task)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = _configuration["Email:Email"],
                    Password = _configuration["Email:Password"]
                };

                client.Credentials = credential;
                client.Host = _configuration["Email:Host"];
                client.Port = int.Parse(_configuration["Email:Port"]);
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress("admin@tech.com"));
                    emailMessage.From = new MailAddress(_configuration["Email:Email"]);
                    emailMessage.Subject = "Status change";
                    emailMessage.Body = $"Status for task '{task.Name}' has changed to {task.Status.ToString()}";
                    client.Send(emailMessage);
                }
            }

            await Task.CompletedTask;
        }
    }
}

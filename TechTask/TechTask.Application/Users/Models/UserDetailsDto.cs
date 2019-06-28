using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Users.Models
{
    public class UserDetailsDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }        
        public string Age { get; set; }
        public string Role { get; set; }
        public List<Tasks> Tasks { get; set; }
        public int NumberOfComments { get; set; }
        public int NumbersOfLogs { get; set; }
        public int? TeamId { get; set; }

        public static Expression<Func<User, UserDetailsDto>> Projection
        {
            get
            {
                return p => new UserDetailsDto
                {
                    Id = p.Id,
                    Age = (DateTime.Now.Year - p.DateOfBirth.Year).ToString(),
                    Name = $"{p.FirstName} {p.LastName}",
                    Email = p.Email,
                    NumberOfComments = p.Comments.Count,
                    NumbersOfLogs = p.Log.Count,
                    Role = p.Role.ToString(),
                    Tasks = p.Tasks,
                    TeamId = p.TeamId
                };
            }
        }

        public static UserDetailsDto ConvertToUserDetails(User user)
        {
            return Projection.Compile().Invoke(user);
        }
    }
}

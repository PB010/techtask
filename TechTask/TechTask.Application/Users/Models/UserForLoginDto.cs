using System;
using System.Linq.Expressions;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Users.Models
{
    public class UserForLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public static Expression<Func<User, UserForLoginDto>> Projection
        {
            get
            {
                return p => new UserForLoginDto
                {
                    Email = p.Email,
                    Password = p.Password
                };
            }
        }

        public static UserForLoginDto ConvertToLoginDto(User user)
        {
            return Projection.Compile().Invoke(user);
        }
    }
}

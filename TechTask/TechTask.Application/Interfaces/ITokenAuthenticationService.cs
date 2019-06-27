using TechTask.Application.Users;
using TechTask.Application.Users.Models;

namespace TechTask.Application.Interfaces
{
    public interface ITokenAuthenticationService
    {
        bool IsAuthenticated(UserForLoginDto user, out string token);
    }
}
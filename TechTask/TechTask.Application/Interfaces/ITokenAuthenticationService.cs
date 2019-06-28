using TechTask.Application.Users.Commands;

namespace TechTask.Application.Interfaces
{
    public interface ITokenAuthenticationService
    {
        string GenerateToken(LoginUserCommand user);
    }
}
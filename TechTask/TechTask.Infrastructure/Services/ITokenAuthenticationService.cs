using TechTask.Application.Users;

namespace TechTask.Infrastructure.Services
{
    public interface ITokenAuthenticationService
    {
        bool IsAuthenticated(UserForLoginDto user, out string token);
    }
}
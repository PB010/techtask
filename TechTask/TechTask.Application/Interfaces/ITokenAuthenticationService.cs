using TechTask.Application.Users.Models;

namespace TechTask.Application.Interfaces
{
    public interface ITokenAuthenticationService
    {
        string GenerateToken(UserForLoginDto user);
        bool UserRoleAdmin();
        bool UserRoleAdminOrEmailMatches(string email);
    }
}
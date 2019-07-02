using TechTask.Application.Users.Models;

namespace TechTask.Application.Interfaces
{
    public interface ITokenAuthenticationService
    {
        string GenerateToken(UserForLoginDto user);
    }
}
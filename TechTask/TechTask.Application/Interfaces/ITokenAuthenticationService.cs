using System;
using TechTask.Application.Users.Models;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Interfaces
{
    public interface ITokenAuthenticationService
    {
        string GenerateToken(UserForLoginDto user);
        bool UserRoleAdmin();
        bool UserRoleAdminOrEmailMatches(string email);
        bool UserRoleAdminOrTeamIdMatches(Team teamFromDb);
        bool UserRoleAdminOrTeamIdMatches(int teamId);
        bool UserRoleAdminOrUserIdMatches(Guid userId);
        string GetUserIdClaimValue();
    }
}
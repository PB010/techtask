using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TechTask.Application.Interfaces;
using TechTask.Application.Users.Models;
using TechTask.Infrastructure.Authentication;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Users;
using TechTask.Persistence.Models.Users.Enums;

namespace TechTask.Infrastructure.Services
{
    public class TokenAuthenticationService : ITokenAuthenticationService
    {
        private readonly TokenManagement _tokenManagement;
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;



        public TokenAuthenticationService(IOptions<TokenManagement> tokenManagement, AppDbContext context,
            IHttpContextAccessor accessor)
        {
            _tokenManagement = tokenManagement.Value;
            _context = context;
            _accessor = accessor;
        }

        public string GenerateToken(UserForLoginDto user)
        {
            var claim = new Claim[4];   
            var userRole = _context.Users.Single(u => u.Email == user.Email &&
                                                      u.Password == user.Password);

            switch (userRole.Role)
            {
                case Roles.Admin:
                    claim[0] = new Claim(ClaimTypes.Email, user.Email);
                    claim[1] = new Claim(ClaimTypes.Role, "Admin");
                    claim[2] = new Claim("TeamId", $"{userRole.TeamId}");
                    claim[3] = new Claim("UserId", $"{userRole.Id}");
                    break;
                case Roles.User:
                    claim[0] = new Claim(ClaimTypes.Email, user.Email);
                    claim[1] = new Claim(ClaimTypes.Role, "User");
                    claim[2] = new Claim("TeamId", $"{userRole.TeamId}");
                    claim[3] = new Claim("UserId", $"{userRole.Id}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claim,
                expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public bool UserRoleAdmin()
        {
            return _accessor.HttpContext.User.IsInRole("Admin");
        }

        public bool UserRoleAdminOrEmailMatches(string email)
        {
            return (_accessor.HttpContext.User.IsInRole("Admin") ||
                   _accessor.HttpContext.User.HasClaim(c => c.Type == ClaimTypes.Email &&
                                                            c.Value == email));
        }

        public bool UserRoleAdminOrTeamIdMatches(Team teamFromDb)
        {
            return (_accessor.HttpContext.User.IsInRole("Admin") ||
                   _accessor.HttpContext.User.HasClaim(c => c.Type == "TeamId" &&
                                                            teamFromDb.Users.Any(t => $"{t.TeamId}" == c.Value)));
        }

        public bool UserRoleAdminOrTeamIdMatches(int teamId)
        {
            return (_accessor.HttpContext.User.IsInRole("Admin") ||
                    _accessor.HttpContext.User.HasClaim(c => c.Type == "TeamId" &&
                                                             c.Value == $"{teamId}"));
        }

        public bool UserRoleAdminOrUserIdMatches(Guid userId)
        {
            return (_accessor.HttpContext.User.IsInRole("Admin") ||
                    _accessor.HttpContext.User.HasClaim(c => c.Type == "UserId" &&
                                                             c.Value == $"{userId}"));
        }

        public string GetUserIdClaimValue()
        {
            return _accessor.HttpContext.User.Claims.Single(c => c.Type == "UserId").Value;
        }
    }
}

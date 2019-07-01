using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TechTask.Application.Interfaces;
using TechTask.Application.Users.Commands;
using TechTask.Infrastructure.Authentication;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Users.Enums;

namespace TechTask.Infrastructure.Services
{
    public class TokenAuthenticationService : ITokenAuthenticationService
    {
        private readonly AppDbContext _context;
        private readonly TokenManagement _tokenManagement;

    

        public TokenAuthenticationService(IOptions<TokenManagement> tokenManagement, AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _tokenManagement = tokenManagement.Value ?? throw new ArgumentNullException(nameof(tokenManagement));
        }

        public string GenerateToken(LoginUserCommand user)
        {
            var claim = new Claim[3];
            var userRole = _context.Users.Single(u => u.Email == user.Email &&
                                                      u.Password == user.Password);

            switch (userRole.Role)
            {
                case Roles.Admin:
                    claim[0] = new Claim(ClaimTypes.Email, user.Email);
                    claim[1] = new Claim(ClaimTypes.Role, "Admin");
                    claim[2] = new Claim("TeamId", $"{userRole.TeamId}");
                    break;
                case Roles.User:
                    claim[0] = new Claim(ClaimTypes.Email, user.Email);
                    claim[1] = new Claim(ClaimTypes.Role, "User");
                    claim[2] = new Claim("TeamId", $"{userRole.TeamId}");
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
    }
}

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechTask.Application.Interfaces;
using TechTask.Application.Users.Commands;
using TechTask.Infrastructure.Authentication;

namespace TechTask.Infrastructure.Services
{
    public class TokenAuthenticationService : ITokenAuthenticationService
    {
        private readonly TokenManagement _tokenManagement;

        public TokenAuthenticationService(IOptions<TokenManagement> tokenManagement)
        {
            _tokenManagement = tokenManagement.Value ?? throw new ArgumentNullException(nameof(tokenManagement));
        }

       public string GenerateToken(LoginUserCommand user)
       {
           var claim = new[]
           {
               new Claim(ClaimTypes.Email, user.Email)
           };
           
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

using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TechTask.API
{
    public class TokenManager
    {
        private static string Secret =
            "uqNwWs4Svd8DWsEnae160OYiRGO9WQDMCVeUhW3Hy84MTZAdZ+AMqsOSxY8DgMcfkx0oOT0pIRGOtpHy7VXWIQ==";

        public static string GenerateToken(string userEmail)
        {
            var key = Convert.FromBase64String(Secret);
            var securityKey = new SymmetricSecurityKey(key);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim(ClaimTypes.Email, userEmail), 
                }),
                Expires = DateTime.UtcNow.AddMinutes(90),
                SigningCredentials = new SigningCredentials(securityKey,
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateJwtSecurityToken(descriptor);

            return handler.WriteToken(token);
        }
    }
}

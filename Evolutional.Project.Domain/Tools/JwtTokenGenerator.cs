using Evolutional.Project.CrossCutting.Configuration;
using Evolutional.Project.Domain.Interfaces.Tools;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Evolutional.Project.Domain.Tools
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public string GenerateToken(string login)
        {
            var symmetricKey = new SymmetricSecurityKey(Convert.FromBase64String(AppSettings.Settings.JwtTokenSettings.SecretKey));
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Authentication, login),
                new Claim(ClaimTypes.Role, "1"),
                new Claim(JwtRegisteredClaimNames.Email, login),
                
            };

            var tokenSpecifications = new JwtSecurityToken(
                    AppSettings.Settings.JwtTokenSettings.Issuer,
                    null,
                    claims,
                    now,
                    now.AddMinutes(AppSettings.Settings.JwtTokenSettings.Expiration),
                    new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(tokenSpecifications);
        }
    }
}
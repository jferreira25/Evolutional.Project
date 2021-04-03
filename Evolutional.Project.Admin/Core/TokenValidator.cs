using Evolutional.Project.CrossCutting.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Evolutional.Project.Admin.Core
{
    public static class TokenValidator
    {
        public static bool Validate(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                if (!(tokenHandler.ReadToken(token) is JwtSecurityToken))
                    return false;

                var symmetricKey = Convert.FromBase64String(AppSettings.Settings.JwtTokenSettings.SecretKey);

                var validationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var _);
                return principal != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error TokenValidator" + ex);
                return false;
            }
        }

        public static string GetGuidToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var canReadToken = tokenHandler.CanReadToken(token);
            if (!canReadToken)
                return null;

            var validationParameters = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(AppSettings.Settings.JwtTokenSettings.SecretKey))
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

            return principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Authentication)?.Value;
        }
    }
}

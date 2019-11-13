using OpenResumeAPI.Helpers.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Principal;
using System;

namespace OpenResumeAPI.Helpers
{
    public class IdentityValidator : IIdentityValidator
    {
        private IAppSettings appSettings;

        public IdentityValidator(IAppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public void ValidateAPI(string key)
        {
            if(!appSettings.APIKeys.Contains(key))
                throw new UnauthorizedAccessException();
        }

        public void ValidateToken(int userId, string token)
        {
            bool result = false;
            string cleanToken = token.Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            SecurityToken securitToken;
            IPrincipal claims = handler.ValidateToken(cleanToken, ValidationSettings(), out securitToken);
            if(claims.Identity.IsAuthenticated)
            {
                result =((ClaimsPrincipal)claims).Claims
                                        .ToList()
                                        .Where(claim => claim.Type.EndsWith("/claims/name"))
                                        .FirstOrDefault()
                                        .Value.Equals(userId.ToString());
            }
            if (!result)
                throw new UnauthorizedAccessException();
        }

        private TokenValidationParameters ValidationSettings()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = appSettings.Issuer,
                ValidAudience = appSettings.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret))
            };
        }

    }
}

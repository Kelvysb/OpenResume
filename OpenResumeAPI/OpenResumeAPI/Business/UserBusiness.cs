using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OpenResumeAPI.Business.Interfaces;
using OpenResumeAPI.Helpers;
using OpenResumeAPI.Models;
using OpenResumeAPI.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OpenResumeAPI.Business
{
    public class UserBusiness : CRUDBusiness<User, IUserRepository>, IUserBusiness
    {

        private IOptions<AppSettings> appSettings;

        public UserBusiness(IUserRepository repository, IOptions<AppSettings> appSettings) : base(repository)
        {
            this.appSettings = appSettings;
        }

        public User Login(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Value.Secret);
            User result = repository.FindByEmail(user.Email);
            result = new User() { Id = 1 };

            if (result != null && result.PasswordHash.Equals(user.PasswordHash))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, result.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                result.Token = tokenHandler.WriteToken(token);
                result.PasswordHash = null;
            }
            else
            {
                result = null;
            }

            return result;
        }

    }
}

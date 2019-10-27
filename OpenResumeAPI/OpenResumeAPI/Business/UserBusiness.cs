using Microsoft.IdentityModel.Tokens;
using OpenResumeAPI.Business.Interfaces;
using OpenResumeAPI.Helpers.Interfaces;
using OpenResumeAPI.Models;
using OpenResumeAPI.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace OpenResumeAPI.Business
{
    public class UserBusiness : CRUDBusiness<User, IUserRepository>, IUserBusiness
    {

        private IAppSettings appSettings;
        private IEmailHelper emailHelper;

        public UserBusiness(IUserRepository repository, IAppSettings appSettings, IEmailHelper emailHelper) : base(repository)
        {
            this.appSettings = appSettings;
            this.emailHelper = emailHelper;
        }

        public User Login(User user)
        {
            User result = repository.FindByEmail(user.Email);
            if (result != null && result.PasswordHash.Equals(user.PasswordHash) && result.EmailConfirmed)
            {
                result.LastActivity = DateTime.UtcNow;
                repository.Update(result);
                result.Token = CreateToken(result.Id);
                result = ClearSecrets(result);
            }
            else
            {
                result = null;
            }
            return result;
        }

        private string CreateToken(int userId)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(appSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userId.ToString())
                    }),
                Issuer = "OpenResumeAPI",
                Audience = "OpenResumeAPI",
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool EmailConfirm(string email, string token)
        {
            bool result = false;
            User user = repository.FindByEmail(email);
            if (user != null && user.ConfirmationToken.Equals(token))
            {
                user.EmailConfirmed = true;
                user.ConfirmationToken = "";
                user.UpdatedDate = DateTime.Now;
                repository.Update(user);
                result = true;
            }
            return result;
        }

        public bool PasswordChange(int userId, string oldPassword, string newPassword)
        {
            bool result = false;
            User user = repository.ByID(userId);
            if (user != null && user.PasswordHash.Equals(oldPassword))
            {
                user.PasswordHash = newPassword;
                user.UpdatedDate = DateTime.Now;
                repository.Update(user);
                result = true;
            }
            return result;
        }

        public override bool Update(User user)
        {
            bool result = false;
            User currentUser = repository.ByID(user.Id);
            if (currentUser != null)
            {
                currentUser.Name = user.Name;
                currentUser.LastName = user.LastName;
                currentUser.UpdatedDate = DateTime.Now;
                repository.Update(user);
                result = true;
            }
            return result;
        }

        public override User ByID(int id)
        {
            return ClearSecrets(base.ByID(id));
        }

        public bool Create(User user)
        {
            bool result = false;
            if (repository.FindByLogin(user.Login) == null)
            {
                user.EmailConfirmed = false;
                user.ConfirmationToken = emailHelper.CreateToken(user);
                user.Id = repository.Insert(user);
                emailHelper.SendEmail(user);
                result = true;
            }
            return result;
        }

        private User ClearSecrets(User user)
        {
            user.PasswordHash = "";
            user.ConfirmationToken = "";
            user.ResetToken = "";
            return user;
        }
    }
}

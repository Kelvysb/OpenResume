using Microsoft.IdentityModel.Tokens;
using OpenResumeAPI.Business.Interfaces;
using OpenResumeAPI.Exceptions;
using OpenResumeAPI.Helpers.Interfaces;
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
                UpdateLastActivity(result);
                result.Token = CreateToken(result.Id);
                result = ClearSecrets(result);
            }
            else
            {
                throw new InvalidLoginException(user);
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

        public void EmailConfirm(string token)
        {
            User user = repository.FindByConfirmation(token);
            if (user != null && !user.EmailConfirmed)
            {
                user.EmailConfirmed = true;
                user.ConfirmationToken = "";
                user.UpdatedDate = DateTime.Now;
                repository.Update(user);
            }
            else
            {
                throw new InvalidTokenException();
            }

        }

        public User PasswordReset(string token)
        {
            User result = null;
            User user = repository.FindByReset(token);
            if (user != null && user.ResetPassword)
            {
                user.ResetPassword = false;
                user.ResetToken = "";
                user.UpdatedDate = DateTime.Now;
                repository.Update(user);
                result = Login(user);
            }
            return result;
        }

        public void ForgetPassword(string email)
        {
            User user = repository.FindByEmail(email);
            if (user != null)
            {
                user.ResetPassword = true;
                user.ResetToken = emailHelper.CreateToken(user);
                repository.Update(user);
                emailHelper.SendResetEmail(user);
            }
            else
            {
                throw new InvalidEmailException();
            }
        }

        public void PasswordChange(int userId, string oldPassword, string newPassword)
        {
            User user = repository.ByID(userId);
            if (user != null && user.PasswordHash.Equals(oldPassword))
            {
                user.PasswordHash = newPassword;
                user.UpdatedDate = DateTime.Now;
                repository.Update(user);
            }
            else
            {
                throw new InvalidLoginException();
            }
        }

        public override void Update(User user)
        {
            User currentUser = repository.ByID(user.Id);
            if (currentUser != null)
            {
                currentUser.Name = user.Name;
                currentUser.LastName = user.LastName;
                currentUser.UpdatedDate = DateTime.Now;
                repository.Update(user);
            }
        }

        public override User ByID(int id)
        {
            return ClearSecrets(base.ByID(id));
        }

        public void Create(User user)
        {
            if (!CheckByLogin(user.Login))
            {
                if(!CheckByEmail(user.Email))
                {
                    user.EmailConfirmed = false;
                    user.ConfirmationToken = emailHelper.CreateToken(user);
                    user.Id = repository.Insert(user);
                    emailHelper.SendConfirmationEmail(user);
                }
                else
                {
                    throw new DuplicateEmailException();
                }
            }
            else
            {
                throw new DuplicateLoginException();
            }
        }

        private bool CheckByLogin(string login)
        {
            try
            {
                repository.FindByLogin(login);
                return true;
            }
            catch(NotFoundException<User>)
            {
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool CheckByEmail(string email)
        {
            try
            {
                repository.FindByEmail(email);
                return true;
            }
            catch (NotFoundException<User>)
            {
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void UpdateLastActivity(User user)
        {
            user.LastActivity = DateTime.UtcNow;
            repository.Update(user);
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

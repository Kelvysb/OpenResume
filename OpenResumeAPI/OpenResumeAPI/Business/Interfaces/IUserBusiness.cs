using OpenResumeAPI.Models;
using System.Net;

namespace OpenResumeAPI.Business.Interfaces
{
    public interface IUserBusiness : ICRUDBusiness<User>
    {
        User Login(User user);
        bool EmailConfirm(string token);
        bool PasswordChange(int userID, string oldPasswrd, string newPassword);
        HttpStatusCode Create(User user);
        User PasswordReset(string token);
        bool ForgetPassword(string email);
    }
}
using OpenResumeAPI.Models;

namespace OpenResumeAPI.Business.Interfaces
{
    public interface IUserBusiness : ICRUDBusiness<User>
    {
        User Login(User user);
        void EmailConfirm(string token);
        void PasswordChange(int userID, string oldPasswrd, string newPassword);
        void Create(User user);
        User PasswordReset(string token);
        void ForgetPassword(string email);
    }
}
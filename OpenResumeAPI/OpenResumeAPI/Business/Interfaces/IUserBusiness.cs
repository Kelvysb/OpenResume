using OpenResumeAPI.Models;

namespace OpenResumeAPI.Business.Interfaces
{
    public interface IUserBusiness : ICRUDBusiness<User>
    {
        User Login(User user);
        bool EmailConfirm(string email, string token);
        bool PasswordChange(int userID, string oldPasswrd, string newPassword);
    }
}
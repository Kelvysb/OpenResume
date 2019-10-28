using OpenResumeAPI.Models;

namespace OpenResumeAPI.Helpers.Interfaces
{
    public interface IEmailHelper
    {
        string CreateToken(User user);
        void SendConfirmationEmail(User user);
        void SendResetEmail(User user);
    }
}
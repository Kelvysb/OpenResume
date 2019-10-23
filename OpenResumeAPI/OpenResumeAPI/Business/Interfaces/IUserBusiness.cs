using OpenResumeAPI.Models;

namespace OpenResumeAPI.Business.Interfaces
{
    public interface IUserBusiness : ICRUDBusiness<User>
    {
        User Login(User user);
    }
}
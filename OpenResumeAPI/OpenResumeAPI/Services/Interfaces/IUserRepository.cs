using OpenResumeAPI.Models;

namespace OpenResumeAPI.Services.Interfaces
{
    public interface IUserRepository: ICRUDRepository<User>
    {
        User FindByEmail(string email);
        User FindByLogin(string login);
        User FindByConfirmation(string token);
        User FindByReset(string token);
    }
}
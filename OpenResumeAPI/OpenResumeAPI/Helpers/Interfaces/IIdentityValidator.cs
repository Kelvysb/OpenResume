namespace OpenResumeAPI.Helpers.Interfaces
{
    public interface IIdentityValidator
    {
        bool Validate(int userId, string token);
    }
}
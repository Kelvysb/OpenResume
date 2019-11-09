namespace OpenResumeAPI.Helpers.Interfaces
{
    public interface IIdentityValidator
    {
        void Validate(int userId, string token);
    }
}
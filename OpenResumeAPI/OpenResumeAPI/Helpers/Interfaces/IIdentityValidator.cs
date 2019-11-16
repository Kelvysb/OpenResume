namespace OpenResumeAPI.Helpers.Interfaces
{
    public interface IIdentityValidator
    {
        void ValidateToken(int userId, string token);
        void ValidateAPI(string key);
    }
}
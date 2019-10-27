namespace OpenResumeAPI.Helpers.Interfaces
{
    public interface IAppSettings
    {
        string ConnectionString { get; set; }
        string Secret { get; set; }
    }
}
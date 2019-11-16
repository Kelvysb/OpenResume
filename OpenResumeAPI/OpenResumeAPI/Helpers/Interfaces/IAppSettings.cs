using System.Collections.Generic;

namespace OpenResumeAPI.Helpers.Interfaces
{
    public interface IAppSettings
    {
        string ConnectionString { get; set; }
        string Secret { get; set; }
        string Issuer { get; set; }
        string Home { get; set; }
        string EmailServer { get; set; }
        int EmailPort { get; set; }
        string EmailUser { get; set; }
        string EmailPassword { get; set; }
        string Email { get; set; }
        string From { get; set; }
        string Subject { get; set; }
        List<string> APIKeys { get; set; }
    }
}
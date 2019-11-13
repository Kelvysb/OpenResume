using Microsoft.Extensions.Options;
using OpenResumeAPI.Helpers.Interfaces;
using System.Collections.Generic;

namespace OpenResumeAPI.Helpers
{
    public class AppSettings : IAppSettings
    {
        public AppSettings()
        {
        }

        public AppSettings(IOptions<AppSettings> appSettings)
        {
            this.ConnectionString = appSettings.Value.ConnectionString;
            this.Secret = appSettings.Value.Secret;
            this.Issuer = appSettings.Value.Issuer;
            this.Home = appSettings.Value.Home;
            this.EmailServer = appSettings.Value.EmailServer;
            this.EmailPort = appSettings.Value.EmailPort;
            this.EmailUser = appSettings.Value.EmailUser;
            this.EmailPassword = appSettings.Value.EmailPassword;
            this.Email = appSettings.Value.Email;
            this.From = appSettings.Value.From;
            this.Subject = appSettings.Value.Subject;
            this.APIKeys = appSettings.Value.APIKeys;
        }

        public string ConnectionString { get; set; }
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Home { get; set; }
        public string EmailServer { get; set; }
        public int EmailPort { get; set; }
        public string EmailUser { get; set; }
        public string EmailPassword { get; set; }
        public string Email { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public List<string> APIKeys { get; set; }
    }
}

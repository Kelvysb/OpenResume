using Microsoft.Extensions.Options;
using OpenResumeAPI.Helpers.Interfaces;

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
        }

        public string ConnectionString { get; set; }
        public string Secret { get; set; }
    }
}

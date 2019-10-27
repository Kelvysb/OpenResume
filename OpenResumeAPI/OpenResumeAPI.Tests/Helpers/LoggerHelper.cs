using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenResumeAPI.Tests.Helpers
{
    class LoggerHelper
    {
        public static ILogger<Control> Create<Control>()
        {
            return LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("OpenResumeAPI.Tests.Program", LogLevel.Debug)
                    .AddConsole();
            }).CreateLogger<Control>();
        }
    }
}

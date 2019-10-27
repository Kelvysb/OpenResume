using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenResumeAPI.Helpers.Interfaces;

namespace OpenResumeAPI.Controllers
{
    public class ControllerBase : Controller
    { 
        protected IIdentityValidator validator;
        protected ILogger<ControllerBase> logger;

        public ControllerBase(ILogger<ControllerBase> logger, IIdentityValidator validator)
        {
            this.logger = logger;
            this.validator = validator;
        }
    }
}

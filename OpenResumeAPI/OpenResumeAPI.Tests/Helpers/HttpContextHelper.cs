using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace OpenResumeAPI.Tests.Helpers
{
    class HttpContextHelper
    {
        public static ControllerContext Create(string Token)
        {
            Mock<IHeaderDictionary> header = new Mock<IHeaderDictionary>();
            header.SetupGet(x => x["Authorization"]).Returns(Token);

            Mock<HttpContext>  httpContext = new Mock<HttpContext>();
            httpContext.Setup(x => x.Request.Headers).Returns(header.Object);
            
            return new ControllerContext() { HttpContext = httpContext.Object}; 
        }
    }
}

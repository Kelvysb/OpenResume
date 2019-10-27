using Moq;
using OpenResumeAPI.Helpers.Interfaces;
using OpenResumeAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenResumeAPI.Tests.Helpers
{
    class EmailSenderHelper
    {
        public static IEmailHelper Create()
        {
            Mock<IEmailHelper> result = new Mock<IEmailHelper>();
            result.Setup(email => email.CreateToken(It.IsAny<User>())).Returns("FAKE_EMAIL_TOKEN");
            return result.Object;
        }
    }
}

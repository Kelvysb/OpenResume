using Moq;
using OpenResumeAPI.Helpers.Interfaces;
using System;

namespace OpenResumeAPI.Tests.Helpers
{
    class ValidatorHelper
    {
        public static IIdentityValidator Create(int validId, string token)
        {
            Mock<IIdentityValidator> validator = new Mock<IIdentityValidator>();
            validator.Setup(valid => valid.Validate(It.Is<int>(id => id == validId), It.Is<string>(input => input.Equals(token))));
            validator.Setup(valid => valid.Validate(It.Is<int>(id => id != validId), It.IsAny<string>())).Throws(new UnauthorizedAccessException());
            validator.Setup(valid => valid.Validate(It.IsAny<int>(), It.Is<string>(input => !input.Equals(token)))).Throws(new UnauthorizedAccessException());
            return validator.Object;
        }
    }
}

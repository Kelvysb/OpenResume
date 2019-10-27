using Moq;
using OpenResumeAPI.Helpers.Interfaces;

namespace OpenResumeAPI.Tests.Helpers
{
    class ValidatorHelper
    {
        public static IIdentityValidator Create(int validId, string token)
        {
            Mock<IIdentityValidator> validator = new Mock<IIdentityValidator>();
            validator.Setup(valid => valid.Validate(validId, token))
                     .Returns(true);
            return validator.Object;
        }
    }
}

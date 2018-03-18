using System;
using System.Linq;
using Xunit;

namespace Crosscutting.Validators.UnitTests
{
    public class ValidationResultsTests
    {
        [Fact]
        public void When_Validation_Success_Then_Should_Return_Empty_MemberNames_List()
        {
            var first = ValidationResults.Success;

            Assert.Empty(first.MemberNames);
        }

        [Fact]
        public void When_Validation_Success_Then_Should_Return_ErrorMessage_Null()
        {
            var first = ValidationResults.Success;

            Assert.Null(first.ErrorMessage);
        }
    }
}


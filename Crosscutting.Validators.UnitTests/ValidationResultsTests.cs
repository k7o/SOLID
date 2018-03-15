using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crosscutting.Validators.UnitTests
{
    [TestClass]
    public class ValidationResultsTests
    {
        [TestMethod]
        public void When_Validation_Success_Then_Should_Return_Empty_MemberNames_List()
        {
            var first = ValidationResults.Success;

            Assert.AreEqual(0, first.MemberNames.Count());
        }

        [TestMethod]
        public void When_Validation_Success_Then_Should_Return_ErrorMessage_Null()
        {
            var first = ValidationResults.Success;

            Assert.IsNull(first.ErrorMessage);
        }
    }
}


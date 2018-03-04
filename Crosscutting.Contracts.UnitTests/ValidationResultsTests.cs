using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crosscutting.Contracts.UnitTests
{
    [TestClass]
    public class ValidationResultsTests
    {
        [TestMethod]
        public void Given_Comparing_2_ValidationResults_Success_When_IsIs_Then_Should_Return_True()
        {
            Assert.IsTrue(ValidationResults.Success == ValidationResults.Success);
        }

        [TestMethod]
        public void Given_Comparing_ValidationResults_Success_And_Reference_To_It_When_Equals_Then_Should_Be_Equal()
        {
            var success1 = ValidationResults.Success;
            var success1Reference = success1;

            Assert.AreEqual(success1Reference, success1);
        }

        [TestMethod]
        public void Given_Comparing_ValidationResults_Success_And_Reference_To_It_When_IsIs_Then_Should_Return_True()
        {
            var success1 = ValidationResults.Success;
            var success1Reference = success1;

            Assert.IsTrue(success1Reference == success1);
        }

        [TestMethod]
        public void Given_Comparing_ValidationResults_Success_And_Succesfull_Joined_ValidatorResult_When_Equals_Then_Should_Not_Be_Equal()
        {
            var success1 = ValidationResults.Success;
            var joinedSuccess1 = ValidationResults.Join(success1, ValidationResults.Success);

            Assert.AreNotEqual(success1, joinedSuccess1);
        }

        [TestMethod]
        public void Given_Comparing_ValidationResults_Success_And_Succesfull_Joined_ValidatorResult_When_IsIs_Then_Should_Return_True()
        {
            var success1 = ValidationResults.Success;
            var joinedSuccess1 = ValidationResults.Join(success1, ValidationResults.Success);

            Assert.IsTrue(success1 == joinedSuccess1);
        }

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


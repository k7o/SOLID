using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure;
using FakeItEasy;
using Contracts;

namespace Implementation.UnitTests.Infrastructure
{
    /// <summary>
    /// Summary description for GuardTests
    /// </summary>
    [TestClass]
    public class GuardTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Should_Throw_ArgumentNullException_When_Object_Is_Null()
        {
            IAmTraceable amTraceable = null;
            try
            {
                Guard.IsNotNull(amTraceable, nameof(amTraceable));
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("amTraceable", ex.ParamName);
                throw;
            }
        }
    }
}

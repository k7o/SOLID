using System.Linq;
using System.Collections.Generic;
using ClassLibrary1.Agents;
using ClassLibrary1.UnitTests.Agents;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ClassLibrary1.Query.Whitelist;
using System;

namespace ClassLibrary1.UnitTests.Query.Whitelist
{
    [TestClass]
    public class ServiceQueryTests
    {
        ServiceAgentResponse _serviceAgentResponse;

        IServiceAgent _serviceAgent;

        Handlers.ServiceQueryHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _serviceAgent = A.Fake<IServiceAgent>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_Throw_ArgumentException_When_The_Same_Bsn_Is_Added()
        {
            _serviceAgentResponse = ServiceAgentResponseBuilder.Construct()
                .With(1)
                .With(1)
                .Build();

            ExecuteHandlerOnSut();
        }

        [TestMethod]
        public void Should_Return_Mapped_ServiceResult()
        {
            _serviceAgentResponse = ServiceAgentResponseBuilder.Construct()
                .With(1, 2000)
                .With(2)
                .With("Het Spant")
                .Build();

            var result = ExecuteHandlerOnSut();

            result.BsnUzovis.Single(c => c.Key == 1 && c.Value == 2000);
            result.BsnUzovis.Single(c => c.Key == 2 && c.Value == 0);
            result.Adresses.Single(c => c == "Het Spant");
        }

        private void CreateSut()
        {
            _sut = new Handlers.ServiceQueryHandler(_serviceAgent);
        }

        private ServiceResult ExecuteHandlerOnSut()
        {
            CreateSut();

            A.CallTo(() => _serviceAgent.Get()).Returns(_serviceAgentResponse);

            return _sut.Handle();
        }
    }
}

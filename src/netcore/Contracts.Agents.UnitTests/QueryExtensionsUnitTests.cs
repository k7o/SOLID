using Business.Contracts.Query.InWhitelist;
using Xunit;

namespace Contracts.Agents.UnitTests
{
    public class QueryExtensionsUnitTests
    {
        [Fact]
        public void Should_Convert_To_QueryString()
        {
            var adresQuery = new BsnUzoviQuery(12, 34);

            Assert.Equal("Bsnnummer=12&Uzovi=34", adresQuery.ToQueryString());
        }
    }
}

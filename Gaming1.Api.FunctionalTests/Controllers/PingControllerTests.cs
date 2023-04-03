using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Gaming1.Api.FunctionalTests.Controllers
{
    [CollectionDefinition("Functional Tests", DisableParallelization = true)]
    public class PingControllerTests : FunctionalTestWebApplicationFactory<TestStartup>
    {
        private HttpClient _client;

        [Fact]
        public async Task Ping_ShouldReturnOk()
        {
            // Arrange
            var url = $"{TestConstants.PingUrl}";

            // Act
            _client = CreateClient();
            var result = await _client.GetAsync(url);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
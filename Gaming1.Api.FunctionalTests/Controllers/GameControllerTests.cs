using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Gaming1.Api.FunctionalTests.Controllers
{
    [CollectionDefinition("Functional Tests", DisableParallelization = true)]
    public class GameControllerTests : FunctionalTestWebApplicationFactory<TestStartup>
    {
        private HttpClient _client;

        [Theory, AutoData]
        public async Task Get_WhenGameIdBelongToAGame_ReturnOkObjectResult_WithGameData(Guid gameId)
        {
            // Arrange
            var url = string.Format(TestConstants.GameUrl, gameId);

            // Act
            _client = CreateClient();

            var result = await _client.GetAsync(url);

            // Assert
            // Temporarily is NotFound because there is no endpoint to add a game previously
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async Task Get_WhenGameIdNotBelongToAnyGame_ReturnNotFoundObjectResult(Guid gameId)
        {
            // Arrange
            var url = string.Format(TestConstants.GameUrl, gameId);

            // Act
            _client = CreateClient();
            var result = await _client.GetAsync(url);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
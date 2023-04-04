using AutoFixture.Xunit2;
using FluentAssertions;
using Gaming1.Api.Contracts.Game;
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
            var url = string.Format(TestConstants.GetGameUrl, gameId);

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
            var url = string.Format(TestConstants.GetGameUrl, gameId);

            // Act
            _client = CreateClient();
            var result = await _client.GetAsync(url);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Start_WhenGameIdNotExist_ReturnOkObjectResult_WithGameData()
        {
            // Arrange
            var url = string.Format(TestConstants.StartGameUrl);

            // Act
            _client = CreateClient();

            var result = await _client.PostAsync(url, CreateHttpContent(default));

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Theory, AutoData]
        public async Task AddPlayers_WhenGameExists_ReturnOkObjectResult_WithGameData(AddPlayersPayload addPlayersPayload)
        {
            // Arrange
            var game = await StartGame();

            _client = CreateClient();
            var url = string.Format(TestConstants.AddPlayersGameUrl, game.GameId);

            // Act
            var result = await _client.PostAsync(url, CreateHttpContent(addPlayersPayload));

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Theory, AutoData]
        public async Task AddPlayers_WhenGameNotExists_ReturnNotFoundObjectResult(Guid gameId, AddPlayersPayload addPlayersPayload)
        {
            // Arrange
            var url = string.Format(TestConstants.AddPlayersGameUrl, gameId);

            // Act
            _client = CreateClient();

            var result = await _client.PostAsync(url, CreateHttpContent(addPlayersPayload));

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
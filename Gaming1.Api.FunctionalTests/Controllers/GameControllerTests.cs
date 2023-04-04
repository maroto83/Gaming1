using AutoFixture.Xunit2;
using FluentAssertions;
using Gaming1.Api.Contracts.Game;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Gaming1.Api.FunctionalTests.Controllers
{
    [CollectionDefinition("Functional Tests", DisableParallelization = true)]
    public class GameControllerTests
        : BaseControllerTests
    {
        [Fact]
        public async Task Get_WhenGameIdBelongToAGame_ReturnOkObjectResult_WithGameData()
        {
            // Arrange
            var gameId = await StartGame();
            var url = string.Format(TestConstants.GetGameUrl, gameId);

            // Act
            var result = await ApiClient.GetAsync(url);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory, AutoData]
        public async Task Get_WhenGameIdNotBelongToAnyGame_ReturnNotFoundObjectResult(Guid gameId)
        {
            // Arrange
            var url = string.Format(TestConstants.GetGameUrl, gameId);

            // Act
            var result = await ApiClient.GetAsync(url);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Start_WhenGameIdNotExist_ReturnOkObjectResult_WithGameData()
        {
            // Arrange
            var url = string.Format(TestConstants.StartGameUrl);

            // Act
            var result = await ApiClient.PostAsync(url, CreateHttpContent(default));

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Theory, AutoData]
        public async Task AddPlayers_WhenGameExists_ReturnOkObjectResult_WithGameData(AddPlayersPayload addPlayersPayload)
        {
            // Arrange
            var gameId = await StartGame();
            var url = string.Format(TestConstants.AddPlayersGameUrl, gameId);

            // Act
            var result = await ApiClient.PostAsync(url, CreateHttpContent(addPlayersPayload));

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Theory, AutoData]
        public async Task AddPlayers_WhenGameNotExists_ReturnNotFoundObjectResult(Guid gameId, AddPlayersPayload addPlayersPayload)
        {
            // Arrange
            var url = string.Format(TestConstants.AddPlayersGameUrl, gameId);

            // Act
            var result = await ApiClient.PostAsync(url, CreateHttpContent(addPlayersPayload));

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async Task Play_WhenGameExists_ReturnOkObjectResult_WithGameData(SuggestNumberPayload suggestNumberPayload)
        {
            // Arrange
            var gameId = await StartGame();

            var playerId = await AddPlayer(gameId);

            var url = string.Format(TestConstants.PlayGameUrl, gameId, playerId);

            // Act
            var result = await ApiClient.PostAsync(url, CreateHttpContent(suggestNumberPayload));

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Theory, AutoData]
        public async Task Play_WhenGameNotExists_ReturnNotFoundObjectResult(
            Guid gameId,
            Guid playerId,
            SuggestNumberPayload suggestNumberPayload)
        {
            // Arrange
            var url = string.Format(TestConstants.PlayGameUrl, gameId, playerId);

            // Act
            var result = await ApiClient.PostAsync(url, CreateHttpContent(suggestNumberPayload));

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
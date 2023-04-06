using AutoFixture.Xunit2;
using FluentAssertions;
using Gaming1.Api.Contracts.Game;
using Gaming1.Api.Controllers;
using Gaming1.Application.Service.Exceptions;
using Gaming1.Application.Services.Contracts.Requests;
using Gaming1.Application.Services.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Gaming1.Api.UnitTests.Controllers
{
    public class GameControllerTest
        : BaseControllerTest<GameController>
    {
        public GameControllerTest()
        {
            Sut =
                new GameController(
                    LoggerMock.Object,
                    MediatorMock.Object,
                    MapperMock.Object);
        }


        [Theory, AutoData]
        public async Task AddPlayers_WhenPayloadIsValid_ReturnCreatedObjectResult(
            Guid gameId,
            AddPlayersResponse addPlayersResponse,
            AddPlayersResult startResult,
            AddPlayersPayload addPlayersPayload)
        {
            // Arrange
            addPlayersResponse.GameId = gameId;

            MediatorMock
                .Setup(x => x.Send(It.IsAny<AddPlayersRequest>(), CancellationToken.None))
                .ReturnsAsync(addPlayersResponse);

            MapperMock
                .Setup(x => x.Map<AddPlayersResult>(addPlayersResponse))
                .Returns(startResult);

            // Act
            var response = await Sut.AddPlayers(gameId, addPlayersPayload);

            // Assert
            response.Should().BeOfType<CreatedAtActionResult>();
        }

        [Theory, AutoData]
        public async Task AddPlayers_WhenCatchAGameNotFoundException_ReturnNotFoundObjectResult(
            Guid gameId,
            GameNotFoundException gameNotFoundException,
            AddPlayersPayload addPlayersPayload)
        {
            // Arrange
            MediatorMock
                .Setup(x => x.Send(It.IsAny<AddPlayersRequest>(), CancellationToken.None))
                .Throws(gameNotFoundException);

            // Act
            var response = await Sut.AddPlayers(gameId, addPlayersPayload);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [Theory, AutoData]
        public async Task AddPlayers_WhenCatchAnException_ReturnConflictObjectResult(
            Guid gameId,
            Exception exception,
            AddPlayersPayload addPlayersPayload)
        {
            // Arrange
            MediatorMock
                .Setup(x => x.Send(It.IsAny<AddPlayersRequest>(), CancellationToken.None))
                .Throws(exception);

            // Act
            var response = await Sut.AddPlayers(gameId, addPlayersPayload);

            // Assert
            response.Should().BeOfType<ConflictObjectResult>();
        }

        [Theory, AutoData]
        public async Task SuggestNumber_WhenPayloadIsValid_ReturnCreatedObjectResult(
            Guid gameId,
            Guid playerId,
            SuggestNumberResponse suggestNumberResponse,
            SuggestNumberResult suggestNumberResult,
            SuggestNumberPayload suggestNumberPayload)
        {
            // Arrange
            MediatorMock
                .Setup(x => x.Send(It.IsAny<SuggestNumberRequest>(), CancellationToken.None))
                .ReturnsAsync(suggestNumberResponse);

            MapperMock
                .Setup(x => x.Map<SuggestNumberResult>(suggestNumberResponse))
                .Returns(suggestNumberResult);

            // Act
            var response = await Sut.SuggestNumber(gameId, playerId, suggestNumberPayload);

            // Assert
            response.Should().BeOfType<CreatedAtActionResult>();
        }

        [Theory, AutoData]
        public async Task SuggestNumber_WhenCatchAGameNotFoundException_ReturnNotFoundObjectResult(
            Guid gameId,
            Guid playerId,
            GameNotFoundException gameNotFoundException,
            SuggestNumberPayload suggestNumberPayload)
        {
            // Arrange
            MediatorMock
                .Setup(x => x.Send(It.IsAny<SuggestNumberRequest>(), CancellationToken.None))
                .Throws(gameNotFoundException);

            // Act
            var response = await Sut.SuggestNumber(gameId, playerId, suggestNumberPayload);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [Theory, AutoData]
        public async Task SuggestNumber_WhenCatchAnException_ReturnConflictObjectResult(
            Guid gameId,
            Guid playerId,
            Exception exception,
            SuggestNumberPayload suggestNumberPayload)
        {
            // Arrange
            MediatorMock
                .Setup(x => x.Send(It.IsAny<SuggestNumberRequest>(), CancellationToken.None))
                .Throws(exception);

            // Act
            var response = await Sut.SuggestNumber(gameId, playerId, suggestNumberPayload);

            // Assert
            response.Should().BeOfType<ConflictObjectResult>();
        }
    }
}
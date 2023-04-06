using AutoFixture.Xunit2;
using FluentAssertions;
using Gaming1.Api.Contracts.Game;
using Gaming1.Api.Controllers.Game;
using Gaming1.Application.Service.Exceptions;
using Gaming1.Application.Services.Contracts.Requests;
using Gaming1.Application.Services.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Gaming1.Api.UnitTests.Controllers.Game
{
    public class PlayGameControllerTest
        : BaseControllerTest<PlayGameController>
    {
        public PlayGameControllerTest()
        {
            Sut =
                new PlayGameController(
                    LoggerMock.Object,
                    MediatorMock.Object,
                    MapperMock.Object);
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
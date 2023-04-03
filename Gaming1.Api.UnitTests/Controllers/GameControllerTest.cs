using AutoFixture.Xunit2;
using FluentAssertions;
using Gaming1.Api.Contracts.Game;
using Gaming1.Api.Controllers;
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
        public async Task Get_WhenGameIdBelongToAGame_ReturnOkObjectResult_WithGameData(
            Guid gameId,
            GetGameResponse getGameResponse,
            GetGameResult getGameResult)
        {
            // Arrange
            MediatorMock
                .Setup(x => x.Send(It.IsAny<GetGameRequest>(), CancellationToken.None))
                .ReturnsAsync(getGameResponse);

            MapperMock
                .Setup(x => x.Map<GetGameResult>(getGameResponse))
                .Returns(getGameResult);

            // Act
            var response = await Sut.Get(gameId);

            // Assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Theory, AutoData]
        public async Task Get_WhenGameIdNotBelongToAnyGame_ReturnNotFoundObjectResult(
            Guid gameId)
        {
            // Arrange
            MediatorMock
                .Setup(x => x.Send(It.IsAny<GetGameRequest>(), CancellationToken.None))
                .ReturnsAsync(default(GetGameResponse));

            MapperMock
                .Setup(x => x.Map<GetGameResult>(default(GetGameResponse)))
                .Returns(default(GetGameResult));

            // Act
            var response = await Sut.Get(gameId);

            // Assert
            response.Should().BeOfType<NotFoundResult>();
        }

        [Theory, AutoData]
        public async Task Get_WhenCatchAnException_ReturnConflictObjectResult(Guid gameId, Exception exception)
        {
            // Arrange
            MediatorMock
                .Setup(x => x.Send(It.IsAny<GetGameRequest>(), CancellationToken.None))
                .Throws(exception);

            // Act
            var response = await Sut.Get(gameId);

            // Assert
            response.Should().BeOfType<ConflictObjectResult>();
        }
    }
}
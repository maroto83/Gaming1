using AutoFixture.Xunit2;
using FluentAssertions;
using Gaming1.Api.Contracts.Game.Results;
using Gaming1.Api.Controllers.Game;
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
    public class StartGameControllerTest
        : BaseControllerTest<StartGameController>
    {
        public StartGameControllerTest()
        {
            Sut =
                new StartGameController(
                    LoggerMock.Object,
                    MediatorMock.Object,
                    MapperMock.Object);
        }


        [Theory, AutoData]
        public async Task Start_WhenGameIdNotExist_ReturnCreatedObjectResult(
            Guid gameId,
            StartResponse startResponse,
            StartResult startResult)
        {
            // Arrange
            startResponse.GameId = gameId;

            MediatorMock
                .Setup(x => x.Send(It.IsAny<StartRequest>(), CancellationToken.None))
                .ReturnsAsync(startResponse);

            MapperMock
                .Setup(x => x.Map<StartResult>(startResponse))
                .Returns(startResult);

            // Act
            var response = await Sut.Start();

            // Assert
            response.Should().BeOfType<CreatedAtActionResult>();
        }

        [Theory, AutoData]
        public async Task Start_WhenCatchAnException_ReturnConflictObjectResult(Exception exception)
        {
            // Arrange
            MediatorMock
                .Setup(x => x.Send(It.IsAny<StartRequest>(), CancellationToken.None))
                .Throws(exception);

            // Act
            var response = await Sut.Start();

            // Assert
            response.Should().BeOfType<ConflictObjectResult>();
        }
    }
}
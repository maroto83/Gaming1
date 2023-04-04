using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Gaming1.Application.Service.Exceptions;
using Gaming1.Application.Service.Handlers;
using Gaming1.Application.Services.Contracts.Requests;
using Gaming1.Application.Services.Contracts.Responses;
using Gaming1.Domain.Models;
using Gaming1.Infrastructure.Repositories;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Gaming1.Application.Service.UnitTests.Handlers
{
    public class AddPlayersRequestHandlerTests
    {
        private readonly AddPlayersRequestHandler _sut;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRepository<Game>> _repositoryMock;

        public AddPlayersRequestHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IRepository<Game>>();

            _sut = new AddPlayersRequestHandler(_mapperMock.Object, _repositoryMock.Object);
        }

        [Theory]
        [AutoData]
        public async Task Handle_WhenRepositoryNotContainsTheModel_Return_Exception(AddPlayersRequest request)
        {
            // Arrange
            _repositoryMock
                .Setup(x => x.Get(It.IsAny<Func<Game, bool>>()))
                .ReturnsAsync(default(Game));

            // Act
            Func<Task> action = async () => await _sut.Handle(request, CancellationToken.None);

            // Assert
            await action.Should()
                .ThrowAsync<GameNotFoundException>()
                .WithMessage($"The Game {request.GameId} is not started.");
        }

        [Theory]
        [AutoData]
        public async Task Handle_WhenRepositoryContainsTheModel_Return_AddPlayersResponse_WithGameData(
            AddPlayersRequest request,
            AddPlayersResponse addPlayersResponse,
            Game game)
        {
            // Arrange
            request.GameId = game.GameId;
            var expectedResult = addPlayersResponse;

            _repositoryMock
                .Setup(x => x.Get(It.IsAny<Func<Game, bool>>()))
                .ReturnsAsync(game);

            _mapperMock
                .Setup(x => x.Map<AddPlayersResponse>(game))
                .Returns(addPlayersResponse);

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(x => x.Save(It.IsAny<Game>()), Times.Once);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
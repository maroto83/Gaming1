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
    public class SuggestNumberRequestHandlerTests
    {
        private readonly SuggestNumberRequestHandler _sut;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRepository<Game>> _repositoryMock;

        public SuggestNumberRequestHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IRepository<Game>>();

            _sut = new SuggestNumberRequestHandler(_mapperMock.Object, _repositoryMock.Object);
        }

        [Theory]
        [AutoData]
        public async Task Handle_WhenRepositoryNotContainsTheModel_Throw_GameNotFoundException(SuggestNumberRequest request)
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
        public async Task Handle_WhenRepositoryContainsTheModel_Return_SuggestNumberResponse_WithGameData(
            SuggestNumberRequest request,
            SuggestNumberResponse suggestNumberResponse,
            Game game)
        {
            // Arrange
            request.GameId = game.GameId;
            var expectedResult = suggestNumberResponse;

            _repositoryMock
                .Setup(x => x.Get(It.IsAny<Func<Game, bool>>()))
                .ReturnsAsync(game);

            _mapperMock
                .Setup(x => x.Map<SuggestNumberResponse>(request))
                .Returns(suggestNumberResponse);

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
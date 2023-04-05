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
    public class GetGameRequestHandlerTests
    {
        private readonly GetGameRequestHandler _sut;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRepository<Game>> _repositoryMock;

        public GetGameRequestHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IRepository<Game>>();

            _sut = new GetGameRequestHandler(_mapperMock.Object, _repositoryMock.Object);
        }

        [Theory]
        [AutoData]
        public async Task Handle__WhenRepositoryContainsTheModel_Return_GetGameResponse(
            GetGameRequest request,
            GetGameResponse getGameResponse,
            Game game)
        {
            // Arrange
            _mapperMock
                .Setup(x => x.Map<GetGameResponse>(game))
                .Returns(getGameResponse);

            _repositoryMock
                .Setup(x => x.Get(It.IsAny<Func<Game, bool>>()))
                .ReturnsAsync(game);

            var expectedResult = getGameResponse;

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Theory]
        [AutoData]
        public async Task Handle_WhenRepositoryNotContainsTheModel_Throw_GameNotFoundException(GetGameRequest request)
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
    }
}
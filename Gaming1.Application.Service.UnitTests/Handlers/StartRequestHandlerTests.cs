﻿using AutoFixture.Xunit2;
using AutoMapper;
using Gaming1.Application.Service.Handlers;
using Gaming1.Application.Service.Services;
using Gaming1.Application.Services.Contracts.Requests;
using Gaming1.Application.Services.Contracts.Responses;
using Gaming1.Domain.Models;
using Gaming1.Infrastructure.Repositories;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Gaming1.Application.Service.UnitTests.Handlers
{
    public class StartRequestHandlerTests
    {
        private readonly StartRequestHandler _sut;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRepository<Game>> _repositoryMock;
        private readonly Mock<ISecretNumberGenerator> _secretNumberGeneratorMock;

        public StartRequestHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IRepository<Game>>();
            _secretNumberGeneratorMock = new Mock<ISecretNumberGenerator>();

            _sut = new StartRequestHandler(_mapperMock.Object, _repositoryMock.Object, _secretNumberGeneratorMock.Object);
        }

        [Theory]
        [AutoData]
        public async Task Handle__WhenRepositoryNotContainsTheModel_Return_StartResponse(
            StartRequest request,
            StartResponse startResponse,
            Game game,
            int secretNumber)
        {
            // Arrange
            _mapperMock
                .Setup(x => x.Map<StartResponse>(game))
                .Returns(startResponse);

            _secretNumberGeneratorMock
                .Setup(x => x.Create(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(secretNumber);

            // Act
            await _sut.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(x => x.Save(It.IsAny<Game>()), Times.Once);
        }
    }
}
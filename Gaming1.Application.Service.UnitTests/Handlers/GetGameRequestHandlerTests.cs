using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Gaming1.Application.Service.Handlers;
using Gaming1.Application.Services.Contracts.Requests;
using Gaming1.Application.Services.Contracts.Responses;
using Gaming1.Domain.Models;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Gaming1.Application.Service.UnitTests.Handlers
{
    public class GetGameRequestHandlerTests
    {
        private readonly GetGameRequestHandler _sut;
        private readonly Mock<IMapper> _mapperMock;
        public GetGameRequestHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _sut = new GetGameRequestHandler(_mapperMock.Object);
        }

        [Theory]
        [AutoData]
        public async Task Handle_GetGameRequest_Return_GetGameResponse(
            GetGameRequest request,
            GetGameResponse getGameResponse)
        {
            // Arrange
            _mapperMock
                .Setup(x => x.Map<GetGameResponse>(It.IsAny<Game>()))
                .Returns(getGameResponse);

            var expectedResult = getGameResponse;

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
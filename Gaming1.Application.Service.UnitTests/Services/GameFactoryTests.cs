using AutoFixture.Xunit2;
using FluentAssertions;
using Gaming1.Application.Service.Services;
using Gaming1.Domain.Models;
using Moq;
using Xunit;

namespace Gaming1.Application.Service.UnitTests.Services
{
    public class GameFactoryTests
    {
        private readonly GameFactory _sut;
        private readonly Mock<ISecretNumberGenerator> _secretNumberGeneratorMock;

        public GameFactoryTests()
        {
            _secretNumberGeneratorMock = new Mock<ISecretNumberGenerator>();

            _sut = new GameFactory(_secretNumberGeneratorMock.Object);
        }

        [Theory, AutoData]
        public void Create_Return_AGameWithSecretNumberGenerated(int secretNumber)
        {
            // Arrange
            _secretNumberGeneratorMock
                .Setup(x => x.Create(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(secretNumber);

            // Act
            var result = _sut.Create();

            // Assert
            result.Should().BeAssignableTo<Game>();
            result.SecretNumber.Should().Be(secretNumber);
        }
    }
}
using AutoFixture.Xunit2;
using FluentAssertions;
using Gaming1.Application.Service.Services;
using Xunit;

namespace Gaming1.Application.Service.UnitTests.Services
{
    public class PlayerGeneratorTests
    {
        private readonly PlayerGenerator _sut;

        public PlayerGeneratorTests()
        {
            _sut = new PlayerGenerator();
        }

        [Theory, AutoData]
        public void Create_Return_NumberOfPlayersGenerated(int playersNumbers)
        {
            // Arrange
            // Act
            var result = _sut.Create(playersNumbers);

            // Assert
            result.Should().HaveCount(playersNumbers);
        }

        [Fact]
        public void Create_Return_2PlayersGeneratedByDefault()
        {
            // Arrange
            // Act
            var result = _sut.Create();

            // Assert
            result.Should().HaveCount(2);
        }
    }
}
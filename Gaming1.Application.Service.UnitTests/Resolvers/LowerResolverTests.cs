using AutoFixture.Xunit2;
using FluentAssertions;
using Gaming1.Application.Service.Resolvers;
using Xunit;

namespace Gaming1.Application.Service.UnitTests.Resolvers
{
    public class LowerResolverTests
    {
        private readonly LowerResolver _sut;

        public LowerResolverTests()
        {
            _sut = new LowerResolver();
        }

        [Theory]
        [AutoData]
        public void CanHandle_WhenSugestedNumberIsHigher_Return_False(
            int secretNumber)
        {
            // Arrange
            var suggestedNumber = secretNumber - 1;

            // Act
            var result = _sut.CanHandle(secretNumber, suggestedNumber);

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [AutoData]
        public void CanHandle_WhenSugestedNumberIsLower_Return_True(
            int secretNumber)
        {
            // Arrange
            var suggestedNumber = secretNumber + 1;

            // Act
            var result = _sut.CanHandle(secretNumber, suggestedNumber);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [AutoData]
        public void CanHandle_WhenSugestedNumberIsEqualToSecretNumber_Return_False(
            int secretNumber)
        {
            // Arrange
            var suggestedNumber = secretNumber;

            // Act
            var result = _sut.CanHandle(secretNumber, suggestedNumber);

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [AutoData]
        public void Handle_Return_TheExpectedResultMessage(
            int secretNumber,
            int suggestedNumber)
        {
            // Arrange
            var expectedResult = $"The secret number is lower than {suggestedNumber}.";

            // Act
            var result = _sut.Handle(secretNumber, suggestedNumber);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
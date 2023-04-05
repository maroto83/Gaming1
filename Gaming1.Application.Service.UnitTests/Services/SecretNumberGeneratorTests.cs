using AutoFixture.Xunit2;
using FluentAssertions;
using Gaming1.Application.Service.Services;
using Xunit;

namespace Gaming1.Application.Service.UnitTests.Services
{
    public class SecretNumberGeneratorTests
    {
        private readonly SecretNumberGenerator _sut;

        public SecretNumberGeneratorTests()
        {
            _sut = new SecretNumberGenerator();
        }

        [Fact]
        public void Create_Return_NumberBetween1And100ByDefault()
        {
            // Arrange
            // Act
            var result = _sut.Create();

            // Assert
            result.Should().BeGreaterOrEqualTo(1);
            result.Should().BeLessOrEqualTo(100);
        }

        [Theory, AutoData]
        public void Create_Return_NumberBetweenMinimumAndMaximum(
            int minimum, int maximum)
        {
            // Arrange
            maximum += minimum;

            // Act
            var result = _sut.Create(minimum, maximum);

            // Assert
            result.Should().BeGreaterOrEqualTo(minimum);
            result.Should().BeLessOrEqualTo(maximum);
        }
    }
}
using FluentAssertions;
using Gaming1.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Gaming1.Api.UnitTests.Controllers
{
    public class PingControllerTest1
    {
        private readonly PingController _sut;

        public PingControllerTest1()
        {
            var loggerMock = new Mock<ILogger<PingController>>();
            _sut = new PingController(loggerMock.Object);
        }


        [Fact]
        public void Ping_ReturnOkObjectResult()
        {
            // Arrange

            // Act
            var response = _sut.Ping();

            // Assert
            response.Should().BeOfType<OkObjectResult>();
        }
    }
}
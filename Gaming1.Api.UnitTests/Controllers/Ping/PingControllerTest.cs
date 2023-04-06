using FluentAssertions;
using Gaming1.Api.Controllers.Ping;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Gaming1.Api.UnitTests.Controllers.Ping
{
    public class PingControllerTest
        : BaseControllerTest<PingController>
    {
        public PingControllerTest()
        {
            Sut = new PingController(LoggerMock.Object);
        }


        [Fact]
        public void Ping_ReturnOkObjectResult()
        {
            // Arrange

            // Act
            var response = Sut.Ping();

            // Assert
            response.Should().BeOfType<OkObjectResult>();
        }
    }
}
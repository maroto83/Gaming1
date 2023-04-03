using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace Gaming1.Api.UnitTests.Controllers
{
    public class BaseControllerTest<TController>
    {
        protected TController Sut;
        protected readonly Mock<IMediator> MediatorMock;
        protected readonly Mock<IMapper> MapperMock;
        protected readonly Mock<ILogger<TController>> LoggerMock;

        public BaseControllerTest()
        {
            LoggerMock = new Mock<ILogger<TController>>();
            MediatorMock = new Mock<IMediator>();
            MapperMock = new Mock<IMapper>();
        }
    }
}
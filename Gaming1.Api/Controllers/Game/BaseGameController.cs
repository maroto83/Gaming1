using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gaming1.Api.Controllers.Game
{
    [ApiController]
    [Route("game")]
    public partial class BaseGameController : ControllerBase
    {
        private readonly ILogger<BaseGameController> Logger;
        protected readonly IMediator Mediator;
        protected readonly IMapper Mapper;

        public BaseGameController(
            ILogger<BaseGameController> logger,
            IMediator mediator,
            IMapper mapper)
        {
            Logger = logger;
            Mediator = mediator;
            Mapper = mapper;
        }
    }
}
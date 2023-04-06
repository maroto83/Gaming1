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
        private readonly ILogger<BaseGameController> _logger;
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;

        public BaseGameController(
            ILogger<BaseGameController> logger,
            IMediator mediator,
            IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }
    }
}
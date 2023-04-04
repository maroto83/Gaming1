using AutoMapper;
using Gaming1.Api.Contracts.Game;
using Gaming1.Application.Services.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Gaming1.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GameController(
            ILogger<GameController> logger,
            IMediator mediator,
            IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{gameId}")]
        [ProducesResponseType(typeof(GetGameResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid gameId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getGameRequest = new GetGameRequest
            {
                GameId = gameId
            };

            try
            {
                var getGameResponse = await _mediator.Send(getGameRequest, CancellationToken.None);

                var getGameResult = _mapper.Map<GetGameResult>(getGameResponse);

                if (getGameResult != default)
                {
                    return Ok(getGameResult);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost("start")]
        [ProducesResponseType(typeof(StartResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Start()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var startRequest = new StartRequest();

            try
            {
                var startResponse = await _mediator.Send(startRequest, CancellationToken.None);

                var startResult = _mapper.Map<StartResult>(startResponse);
                return CreatedAtAction(
                    nameof(Get),
                    new { gameId = startResult.GameId },
                    startResult);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
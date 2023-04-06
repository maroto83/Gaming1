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

namespace Gaming1.Api.Controllers.Game
{
    public class StartGameController
        : BaseGameController
    {
        public StartGameController(
            ILogger<BaseGameController> logger,
            IMediator mediator,
            IMapper mapper)
            : base(logger, mediator, mapper)
        {
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
                    "Get",
                    "GetGame",
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
using AutoMapper;
using Gaming1.Api.Contracts.Game;
using Gaming1.Application.Service.Exceptions;
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
    public class GetGameController
        : BaseGameController
    {
        public GetGameController(
            ILogger<BaseGameController> logger,
            IMediator mediator,
            IMapper mapper)
            : base(logger, mediator, mapper)
        {
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
                var getGameResponse = await Mediator.Send(getGameRequest, CancellationToken.None);

                var getGameResult = Mapper.Map<GetGameResult>(getGameResponse);

                return Ok(getGameResult);
            }
            catch (GameNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
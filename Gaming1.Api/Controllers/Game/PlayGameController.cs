using AutoMapper;
using Gaming1.Api.Contracts.Game.Payloads;
using Gaming1.Api.Contracts.Game.Results;
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
    public class PlayGameController
        : BaseGameController
    {
        public PlayGameController(
            ILogger<BaseGameController> logger,
            IMediator mediator,
            IMapper mapper)
            : base(logger, mediator, mapper)
        {
        }

        [HttpPost("{gameId}/players/{playerId}/play")]
        [ProducesResponseType(typeof(SuggestNumberResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SuggestNumber(Guid gameId, Guid playerId, [FromBody] SuggestNumberPayload suggestNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var suggestNumberRequest = new SuggestNumberRequest
            {
                GameId = gameId,
                PlayerId = playerId,
                SuggestedNumber = suggestNumber.SuggestedNumber
            };

            try
            {
                var suggestNumberResponse = await Mediator.Send(suggestNumberRequest, CancellationToken.None);

                var suggestNumberResult = Mapper.Map<SuggestNumberResult>(suggestNumberResponse);

                return CreatedAtAction(
                    "Get",
                    "GetGame",
                    new { gameId = suggestNumberResult.GameId },
                    suggestNumberResult);
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
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

        [HttpPost("{gameId}/players/add")]
        [ProducesResponseType(typeof(AddPlayersResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddPlayers(Guid gameId, [FromBody] AddPlayersPayload addPlayers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addPlayersRequest = new AddPlayersRequest
            {
                GameId = gameId,
                PlayersNumber = addPlayers.PlayersNumber
            };

            try
            {
                var addPlayersResponse = await _mediator.Send(addPlayersRequest, CancellationToken.None);

                var addPlayersResult = _mapper.Map<AddPlayersResult>(addPlayersResponse);

                return CreatedAtAction(
                    "Get",
                    "GetGame",
                    new { gameId = addPlayersResult.GameId },
                    addPlayersResult);
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

        [HttpPost("{gameId}/players/{playerId}/play")]
        [ProducesResponseType(typeof(AddPlayersResult), (int)HttpStatusCode.OK)]
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
                var suggestNumberResponse = await _mediator.Send(suggestNumberRequest, CancellationToken.None);

                var suggestNumberResult = _mapper.Map<SuggestNumberResult>(suggestNumberResponse);

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
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
    public class AddPlayersController
        : BaseGameController
    {
        public AddPlayersController(
            ILogger<BaseGameController> logger,
            IMediator mediator,
            IMapper mapper)
            : base(logger, mediator, mapper)
        {
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
                var addPlayersResponse = await Mediator.Send(addPlayersRequest, CancellationToken.None);

                var addPlayersResult = Mapper.Map<AddPlayersResult>(addPlayersResponse);

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
    }
}
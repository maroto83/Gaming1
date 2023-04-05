using AutoMapper;
using Gaming1.Application.Services.Contracts.Requests;
using Gaming1.Application.Services.Contracts.Responses;
using Gaming1.Domain.Models;
using Gaming1.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gaming1.Application.Service.Handlers
{
    public class AddPlayersRequestHandler : BaseRequestHandler<AddPlayersRequest, AddPlayersResponse>
    {
        public AddPlayersRequestHandler(IMapper mapper, IRepository<Game> repository)
            : base(mapper, repository)
        {
        }

        protected override async Task<AddPlayersResponse> HandleRequest(AddPlayersRequest request)
        {
            var game = await GetGame(request.GameId);

            game.Players = new List<Player>();
            for (var i = 0; i < request.PlayersNumber; i++)
            {
                var player =
                    new Player
                    {
                        PlayerId = Guid.NewGuid()
                    };

                game.Players.Add(player);
            }

            await Repository.Save(game);

            var addPlayersResponse = Mapper.Map<AddPlayersResponse>(game);

            return addPlayersResponse;
        }
    }
}
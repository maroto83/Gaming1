using AutoMapper;
using Gaming1.Application.Service.Exceptions;
using Gaming1.Application.Services.Contracts.Requests;
using Gaming1.Application.Services.Contracts.Responses;
using Gaming1.Domain.Models;
using Gaming1.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gaming1.Application.Service.Handlers
{
    public class AddPlayersRequestHandler : IRequestHandler<AddPlayersRequest, AddPlayersResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Game> _repository;

        public AddPlayersRequestHandler(
            IMapper mapper,
            IRepository<Game> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<AddPlayersResponse> Handle(AddPlayersRequest request, CancellationToken cancellationToken)
        {
            var game = await _repository.Get(x => x.GameId.Equals(request.GameId));

            if (game == default)
            {
                throw new GameNotFoundException(request.GameId);
            }

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

            await _repository.Save(game);

            var addPlayersResponse = _mapper.Map<AddPlayersResponse>(game);

            return addPlayersResponse;
        }
    }
}
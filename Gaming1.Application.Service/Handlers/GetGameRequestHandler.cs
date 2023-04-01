using AutoMapper;
using Gaming1.Application.Services.Contracts.Requests;
using Gaming1.Application.Services.Contracts.Responses;
using Gaming1.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gaming1.Application.Service.Handlers
{
    public class GetGameRequestHandler : IRequestHandler<GetGameRequest, GetGameResponse>
    {
        private readonly IMapper _mapper;

        public GetGameRequestHandler(
            IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<GetGameResponse> Handle(GetGameRequest request, CancellationToken cancellationToken)
        {
            var game = new Game
            {
                GameId = Guid.NewGuid(),
                Players =
                    new List<Player>
                    {
                        new Player
                        {
                            PlayerId = Guid.NewGuid()
                        }
                    },
                SecretNumber = new Random().Next(1, 100)
            };

            var getGameResponse = _mapper.Map<GetGameResponse>(game);

            return getGameResponse;
        }
    }
}
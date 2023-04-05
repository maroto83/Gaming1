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
    public class StartRequestHandler : BaseRequestHandler<StartRequest, StartResponse>
    {
        public StartRequestHandler(IMapper mapper, IRepository<Game> repository)
            : base(mapper, repository)
        {
        }

        protected override async Task<StartResponse> HandleRequest(StartRequest request)
        {
            var game
                = new Game
                {
                    GameId = Guid.NewGuid(),
                    Players = new List<Player>(),
                    SecretNumber = new Random().Next(1, 100)
                };

            await Repository.Save(game);

            var startResponse = Mapper.Map<StartResponse>(game);

            return startResponse;
        }
    }
}
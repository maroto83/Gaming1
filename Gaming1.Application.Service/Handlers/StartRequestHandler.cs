using AutoMapper;
using Gaming1.Application.Service.Services;
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
        private readonly ISecretNumberGenerator _secretNumberGenerator;

        public StartRequestHandler(
            IMapper mapper,
            IRepository<Game> repository,
            ISecretNumberGenerator secretNumberGenerator)
            : base(mapper, repository)
        {
            _secretNumberGenerator = secretNumberGenerator;
        }

        protected override async Task<StartResponse> HandleRequest(StartRequest request)
        {
            var game
                = new Game
                {
                    GameId = Guid.NewGuid(),
                    Players = new List<Player>(),
                    SecretNumber = _secretNumberGenerator.Create()
                };

            await Repository.Save(game);

            var startResponse = Mapper.Map<StartResponse>(game);

            return startResponse;
        }
    }
}
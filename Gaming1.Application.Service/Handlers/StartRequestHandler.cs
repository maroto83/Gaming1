using AutoMapper;
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
    public class StartRequestHandler : IRequestHandler<StartRequest, StartResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Game> _repository;

        public StartRequestHandler(
            IMapper mapper,
            IRepository<Game> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<StartResponse> Handle(StartRequest request, CancellationToken cancellationToken)
        {
            var game
                = new Game
                {
                    GameId = Guid.NewGuid(),
                    Players = new List<Player>(),
                    SecretNumber = new Random().Next(1, 100)
                };

            await _repository.Save(game);

            var startResponse = _mapper.Map<StartResponse>(game);

            return startResponse;
        }
    }
}
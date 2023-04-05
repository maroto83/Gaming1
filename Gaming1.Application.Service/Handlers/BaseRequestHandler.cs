using AutoMapper;
using Gaming1.Application.Service.Exceptions;
using Gaming1.Domain.Models;
using Gaming1.Infrastructure.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gaming1.Application.Service.Handlers
{
    public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly IMapper Mapper;
        protected readonly IRepository<Game> Repository;

        protected BaseRequestHandler(
            IMapper mapper,
            IRepository<Game> repository)
        {
            Mapper = mapper;
            Repository = repository;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            return HandleRequest(request);
        }

        protected abstract Task<TResponse> HandleRequest(TRequest request);

        protected async Task<Game> GetGame(Guid gameId)
        {
            var game = await Repository.Get(x => x.GameId.Equals(gameId));

            if (game == default)
            {
                throw new GameNotFoundException(gameId);
            }

            return game;
        }
    }
}
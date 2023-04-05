using AutoMapper;
using Gaming1.Application.Service.Resolvers;
using Gaming1.Application.Services.Contracts.Requests;
using Gaming1.Application.Services.Contracts.Responses;
using Gaming1.Domain.Models;
using Gaming1.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Gaming1.Application.Service.Handlers
{
    public class SuggestNumberRequestHandler : BaseRequestHandler<SuggestNumberRequest, SuggestNumberResponse>
    {
        private readonly IGameResolver _gameResolver;

        public SuggestNumberRequestHandler(
            IMapper mapper,
            IRepository<Game> repository,
            IGameResolver gameResolver)
            : base(mapper, repository)
        {
            _gameResolver = gameResolver;
        }

        protected override async Task<SuggestNumberResponse> HandleRequest(SuggestNumberRequest request)
        {
            var game = await GetGame(request.GameId);

            var resultMessage = _gameResolver.Resolve(game.SecretNumber, request.SuggestedNumber);

            var suggestNumberResponse = Mapper.Map<SuggestNumberResponse>(request);
            suggestNumberResponse.ResultMessage = resultMessage;

            return suggestNumberResponse;
        }
    }
}
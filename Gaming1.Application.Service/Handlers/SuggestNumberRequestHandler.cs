using AutoMapper;
using Gaming1.Application.Service.Exceptions;
using Gaming1.Application.Services.Contracts.Requests;
using Gaming1.Application.Services.Contracts.Responses;
using Gaming1.Domain.Models;
using Gaming1.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gaming1.Application.Service.Handlers
{
    public class SuggestNumberRequestHandler : IRequestHandler<SuggestNumberRequest, SuggestNumberResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Game> _repository;

        public SuggestNumberRequestHandler(
            IMapper mapper,
            IRepository<Game> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<SuggestNumberResponse> Handle(SuggestNumberRequest request, CancellationToken cancellationToken)
        {
            var game = await _repository.Get(x => x.GameId.Equals(request.GameId));

            if (game == default)
            {
                throw new GameNotFoundException(request.GameId);
            }

            var resultMessage = $"Yes! The secret number is {request.SuggestedNumber}. The player {request.PlayerId} is the winner!";
            if (game.SecretNumber > request.SuggestedNumber)
            {
                resultMessage = $"The secret number is higher than {request.SuggestedNumber}";
            }
            else if (game.SecretNumber < request.SuggestedNumber)
            {
                resultMessage = $"The secret number is lower than {request.SuggestedNumber}";
            }

            var suggestNumberResponse = _mapper.Map<SuggestNumberResponse>(request);
            suggestNumberResponse.ResultMessage = resultMessage;

            return suggestNumberResponse;
        }
    }
}
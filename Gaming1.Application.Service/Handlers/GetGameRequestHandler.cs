using AutoMapper;
using Gaming1.Application.Services.Contracts.Requests;
using Gaming1.Application.Services.Contracts.Responses;
using Gaming1.Domain.Models;
using Gaming1.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Gaming1.Application.Service.Handlers
{
    public class GetGameRequestHandler : IRequestHandler<GetGameRequest, GetGameResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Game> _repository;

        public GetGameRequestHandler(
            IMapper mapper,
            IRepository<Game> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GetGameResponse> Handle(GetGameRequest request, CancellationToken cancellationToken)
        {
            var game = await _repository.Get(x => x.GameId.Equals(request.GameId));

            var getGameResponse = _mapper.Map<GetGameResponse>(game);

            return getGameResponse;
        }
    }
}
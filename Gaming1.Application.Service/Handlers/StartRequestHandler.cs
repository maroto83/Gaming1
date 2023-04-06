using AutoMapper;
using Gaming1.Application.Service.Services;
using Gaming1.Application.Services.Contracts.Requests;
using Gaming1.Application.Services.Contracts.Responses;
using Gaming1.Domain.Models;
using Gaming1.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Gaming1.Application.Service.Handlers
{
    public class StartRequestHandler : BaseRequestHandler<StartRequest, StartResponse>
    {
        private readonly IGameFactory _gameFactory;

        public StartRequestHandler(
            IMapper mapper,
            IRepository<Game> repository,
            IGameFactory gameFactory)
            : base(mapper, repository)
        {
            _gameFactory = gameFactory;
        }

        protected override async Task<StartResponse> HandleRequest(StartRequest request)
        {
            var game = _gameFactory.Create();

            await Repository.Save(game);

            var startResponse = Mapper.Map<StartResponse>(game);

            return startResponse;
        }
    }
}
using AutoMapper;
using Gaming1.Application.Service.Services;
using Gaming1.Application.Services.Contracts.Requests;
using Gaming1.Application.Services.Contracts.Responses;
using Gaming1.Domain.Models;
using Gaming1.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Gaming1.Application.Service.Handlers
{
    public class AddPlayersRequestHandler : BaseRequestHandler<AddPlayersRequest, AddPlayersResponse>
    {
        private readonly IPlayerGenerator _playerGenerator;
        public AddPlayersRequestHandler(
            IMapper mapper,
            IRepository<Game> repository,
            IPlayerGenerator playerGenerator)
            : base(mapper, repository)
        {
            _playerGenerator = playerGenerator;
        }

        protected override async Task<AddPlayersResponse> HandleRequest(AddPlayersRequest request)
        {
            var game = await GetGame(request.GameId);

            game.Players = _playerGenerator.Create(request.PlayersNumber);

            await Repository.Save(game);

            var addPlayersResponse = Mapper.Map<AddPlayersResponse>(game);

            return addPlayersResponse;
        }
    }
}
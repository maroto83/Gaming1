using AutoMapper;
using Gaming1.Application.Services.Contracts.Requests;
using Gaming1.Application.Services.Contracts.Responses;
using Gaming1.Domain.Models;
using Gaming1.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Gaming1.Application.Service.Handlers
{
    public class GetGameRequestHandler : BaseRequestHandler<GetGameRequest, GetGameResponse>
    {
        public GetGameRequestHandler(IMapper mapper, IRepository<Game> repository)
            : base(mapper, repository)
        {
        }

        protected override async Task<GetGameResponse> HandleRequest(GetGameRequest request)
        {
            var game = await GetGame(request.GameId);

            var getGameResponse = Mapper.Map<GetGameResponse>(game);

            return getGameResponse;
        }
    }
}
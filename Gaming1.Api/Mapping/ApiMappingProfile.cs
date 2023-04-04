using AutoMapper;
using Gaming1.Api.Contracts.Game;
using Gaming1.Application.Services.Contracts.Responses;

namespace Gaming1.Api.Mapping
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<GetGameResponse, GetGameResult>();
            CreateMap<StartResponse, StartResult>();
            CreateMap<AddPlayersResponse, AddPlayersResult>();
            CreateMap<SuggestNumberResponse, SuggestNumberResult>();
            CreateMap<PlayerResponse, PlayerResult>();
        }
    }
}
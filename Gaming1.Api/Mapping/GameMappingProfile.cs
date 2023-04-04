using AutoMapper;
using Gaming1.Api.Contracts.Game;
using Gaming1.Application.Services.Contracts.Responses;

namespace Gaming1.Api.Mapping
{
    public class GameMappingProfile : Profile
    {
        public GameMappingProfile()
        {
            CreateMap<GetGameResponse, GetGameResult>();
            CreateMap<StartResponse, StartResult>();
            CreateMap<PlayerResponse, PlayerResult>();
        }
    }
}
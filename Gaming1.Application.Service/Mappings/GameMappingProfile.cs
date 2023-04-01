using AutoMapper;
using Gaming1.Application.Services.Contracts.Responses;
using Gaming1.Domain.Models;

namespace Gaming1.Application.Service.Mappings
{
    public class GameMappingProfile : Profile
    {
        public GameMappingProfile()
        {
            CreateMap<Game, GetGameResponse>();
            CreateMap<Player, PlayerResponse>();
        }
    }
}
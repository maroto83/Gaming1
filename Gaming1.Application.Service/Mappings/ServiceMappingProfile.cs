using AutoMapper;
using Gaming1.Application.Services.Contracts.Requests;
using Gaming1.Application.Services.Contracts.Responses;
using Gaming1.Domain.Models;

namespace Gaming1.Application.Service.Mappings
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<Game, GetGameResponse>();
            CreateMap<Game, StartResponse>();
            CreateMap<Game, AddPlayersResponse>();

            CreateMap<SuggestNumberRequest, SuggestNumberResponse>()
                .ForMember(d => d.ResultMessage, opt => opt.Ignore());

            CreateMap<Player, PlayerResponse>();
        }
    }
}
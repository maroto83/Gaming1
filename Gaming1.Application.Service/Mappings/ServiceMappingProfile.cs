﻿using AutoMapper;
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
            CreateMap<Player, PlayerResponse>();
        }
    }
}
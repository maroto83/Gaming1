using System;
using System.Collections.Generic;

namespace Gaming1.Application.Services.Contracts.Responses
{
    public class GetGameResponse
    {
        public Guid GameId { get; set; }
        public List<PlayerResponse> Players { get; set; }
    }
}
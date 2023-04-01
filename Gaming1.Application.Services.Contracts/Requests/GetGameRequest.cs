using Gaming1.Application.Services.Contracts.Responses;
using MediatR;
using System;

namespace Gaming1.Application.Services.Contracts.Requests
{
    public class GetGameRequest : IRequest<GetGameResponse>
    {
        public Guid GameId { get; set; }
    }
}
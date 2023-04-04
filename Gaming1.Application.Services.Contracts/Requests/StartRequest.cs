using Gaming1.Application.Services.Contracts.Responses;
using MediatR;
using System;

namespace Gaming1.Application.Services.Contracts.Requests
{
    public class StartRequest : IRequest<StartResponse>
    {
        public Guid GameId { get; set; }
    }
}
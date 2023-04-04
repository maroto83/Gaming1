using Gaming1.Application.Services.Contracts.Responses;
using MediatR;
using System;

namespace Gaming1.Application.Services.Contracts.Requests
{
    public class SuggestNumberRequest : IRequest<SuggestNumberResponse>
    {
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public int SuggestedNumber { get; set; }
    }
}
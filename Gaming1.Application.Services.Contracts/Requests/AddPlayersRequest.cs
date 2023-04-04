using Gaming1.Application.Services.Contracts.Responses;
using MediatR;
using System;

namespace Gaming1.Application.Services.Contracts.Requests
{
    public class AddPlayersRequest : IRequest<AddPlayersResponse>
    {
        public int PlayersNumber { get; set; }
        public Guid GameId { get; set; }
    }
}
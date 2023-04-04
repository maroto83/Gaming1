using System;

namespace Gaming1.Application.Services.Contracts.Responses
{
    public class SuggestNumberResponse
    {
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public int SuggestedNumber { get; set; }
        public string ResultMessage { get; set; }
    }
}
using System;

namespace Gaming1.Api.Contracts.Game.Results
{
    public class SuggestNumberResult
    {
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public int SuggestedNumber { get; set; }
        public string ResultMessage { get; set; }
    }
}
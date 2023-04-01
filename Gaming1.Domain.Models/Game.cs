using System;
using System.Collections.Generic;

namespace Gaming1.Domain.Models
{
    public class Game
    {
        public Guid GameId { get; set; }
        public List<Player> Players { get; set; }
        public int SecretNumber { get; set; }
    }
}
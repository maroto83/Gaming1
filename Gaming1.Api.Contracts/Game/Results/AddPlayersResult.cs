﻿using System;
using System.Collections.Generic;

namespace Gaming1.Api.Contracts.Game.Results
{
    public class AddPlayersResult
    {
        public Guid GameId { get; set; }
        public List<PlayerResult> Players { get; set; }
    }
}
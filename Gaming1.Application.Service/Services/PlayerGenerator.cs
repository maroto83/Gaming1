using Gaming1.Domain.Models;
using System;
using System.Collections.Generic;

namespace Gaming1.Application.Service.Services
{
    public class PlayerGenerator
        : IPlayerGenerator
    {
        public List<Player> Create(int playersNumbers = 2)
        {
            var players = new List<Player>();

            for (var i = 0; i < playersNumbers; i++)
            {
                var player =
                    new Player
                    {
                        PlayerId = Guid.NewGuid()
                    };

                players.Add(player);
            }

            return players;
        }
    }
}
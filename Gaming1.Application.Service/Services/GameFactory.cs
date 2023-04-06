using Gaming1.Domain.Models;
using System;
using System.Collections.Generic;

namespace Gaming1.Application.Service.Services
{
    public class GameFactory
        : IGameFactory
    {
        private readonly ISecretNumberGenerator _secretNumberGenerator;

        public GameFactory(ISecretNumberGenerator secretNumberGenerator)
        {
            _secretNumberGenerator = secretNumberGenerator;
        }

        public Game Create()
        {
            var game
                = new Game
                {
                    GameId = Guid.NewGuid(),
                    Players = new List<Player>(),
                    SecretNumber = _secretNumberGenerator.Create()
                };

            return game;
        }
    }
}
using System;

namespace Gaming1.Application.Service.Exceptions
{
    public class GameNotFoundException
        : Exception
    {
        public GameNotFoundException(Guid gameId)
            : base($"The Game {gameId} is not started.")
        {
        }
    }
}
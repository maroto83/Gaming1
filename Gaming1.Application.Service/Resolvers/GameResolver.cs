using System;

namespace Gaming1.Application.Service.Resolvers
{
    public abstract class GameResolver : IGameResolver
    {
        private IGameResolver _nextResolver;

        public IGameResolver SetNext(IGameResolver nextResolver)
        {
            _nextResolver = nextResolver;
            return nextResolver;
        }

        public string Resolve(int secretNumber, int suggestedNumber)
        {
            if (CanHandle(secretNumber, suggestedNumber))
            {
                return Handle(secretNumber, suggestedNumber);
            }

            if (_nextResolver != null)
            {
                return _nextResolver.Resolve(secretNumber, suggestedNumber);
            }

            throw new Exception("There is no resolver setup properly.");
        }

        public abstract string Handle(int secretNumber, int suggestedNumber);

        public abstract bool CanHandle(int secretNumber, int suggestedNumber);
    }
}
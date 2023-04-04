namespace Gaming1.Application.Service.Resolvers
{
    public interface IGameResolver
        : INextResolver<IGameResolver>
    {
        string Resolve(int secretNumber, int suggestedNumber);
    }
}
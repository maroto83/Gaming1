namespace Gaming1.Application.Service.Resolvers
{
    public interface INextResolver<T> where T : class
    {
        T SetNext(T nextResolver);
    }
}
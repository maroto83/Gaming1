using System.Collections.Generic;
using System.Linq;

namespace Gaming1.Application.Service.Resolvers
{
    public class ChainOfResponsabilityBuilder<T>
        where T : class, INextResolver<T>
    {
        private readonly List<T> _processors;

        public ChainOfResponsabilityBuilder()
        {
            _processors = new List<T>();
        }

        public ChainOfResponsabilityBuilder<T> RegisterProcessor(T processor)
        {
            _processors.Add(processor);
            return this;
        }

        public T Build()
        {
            using var enumerator = _processors.GetEnumerator();

            var currentProcessor = enumerator.Current;
            while (enumerator.MoveNext())
            {
                if (currentProcessor != null)
                {
                    currentProcessor.SetNext(enumerator.Current);
                    currentProcessor = enumerator.Current;
                }
                else
                {
                    currentProcessor = enumerator.Current;
                }
            }

            return _processors.First();
        }
    }
}
using System.Collections.Generic;

namespace Pipelining
{
    public abstract class ConsumerBase<T> : IConsumer<T>
    {
        public void Consume(IEnumerable<T> value)
        {
            foreach (var item in value)
                Consume(item);
        }

        public abstract void Consume(T value);
    }
}
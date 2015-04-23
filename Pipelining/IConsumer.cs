using System.Collections.Generic;

namespace Pipelining
{
    public interface IConsumer<T>
    {
        void Consume(IEnumerable<T> value);
    }
}
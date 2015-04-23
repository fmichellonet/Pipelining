using System.Collections.Generic;

namespace Pipelining
{
    public interface IGenerator<T>
    {
        IEnumerable<T> Generate();
        ITransformer<T, TNext> Pipe<TNext>(ITransformer<T, TNext> next);
        TerminatedPipe<T> Pipe(IConsumer<T> next);
    }
}
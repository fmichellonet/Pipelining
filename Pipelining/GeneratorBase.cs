using System.Collections.Generic;

namespace Pipelining
{
    public abstract class GeneratorBase<T> : IGenerator<T>
    {
        public abstract IEnumerable<T> Generate();

        public ITransformer<T, TNext> Pipe<TNext>(ITransformer<T, TNext> next)
        {
            return new TransformingPipe<T, TNext>(this, next);
        }

        public TerminatedPipe<T> Pipe(IConsumer<T> next)
        {
            return new TerminatedPipe<T>(this, next);
        }
    }
}
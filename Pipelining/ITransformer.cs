using System.Collections.Generic;

namespace Pipelining
{
    public interface ITransformer<TIn, TOut> : IConsumer<TIn>, IGenerator<TOut>
    {
        IEnumerable<TOut> Transform(IEnumerable<TIn> values);
        IEnumerable<TOut> Transform();
    }
}
using System.Collections.Generic;

namespace Pipelining
{
    public abstract class TransformerBase<TIn, TOut> : ITransformer<TIn, TOut>
    {
        private IEnumerable<TIn> _value;
        public abstract IEnumerable<TOut> Transform(IEnumerable<TIn> values);

        public IEnumerable<TOut> Transform()
        {
            return Transform(null);
        }

        public void Consume(IEnumerable<TIn> value)
        {
            _value = value;
        }

        public IEnumerable<TOut> Generate()
        {
            var res = Transform(_value);
            foreach (var item in res)
                yield return item;
        }

        public ITransformer<TOut, TNext> Pipe<TNext>(ITransformer<TOut, TNext> next)
        {
            return new TransformingPipe<TOut, TNext>(this, next);
        }

        public TerminatedPipe<TOut> Pipe(IConsumer<TOut> next)
        {
            return new TerminatedPipe<TOut>(this, next);
        }
    }
}
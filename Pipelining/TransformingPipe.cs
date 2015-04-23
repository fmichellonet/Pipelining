using System.Collections.Generic;

namespace Pipelining
{
    internal class TransformingPipe<T, TNext> : TransformerBase<T, TNext>
    {
        private readonly ITransformer<T, TNext> _destination;
        private readonly IGenerator<T> _source;

        public TransformingPipe(IGenerator<T> source, ITransformer<T, TNext> destination)
        {
            _source = source;
            _destination = destination;
        }

        public override IEnumerable<TNext> Transform(IEnumerable<T> values)
        {
            var val = _source.Generate();
            var res = _destination.Transform(val);
            return res;
        }
    }
}
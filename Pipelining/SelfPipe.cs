using System.Collections.Generic;

namespace Pipelining
{
    internal class SelfPipe<T> : TransformerBase<T, T>
    {
        private readonly IConsumer<T> _destination;
        private readonly IGenerator<T> _source;

        public SelfPipe(IGenerator<T> source, IConsumer<T> destination)
        {
            _source = source;
            _destination = destination;
        }

        public override IEnumerable<T> Transform(IEnumerable<T> input)
        {
            var val = _source.Generate();
            _destination.Consume(val);
            return val;
        }
    }
}
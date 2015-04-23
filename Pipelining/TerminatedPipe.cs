using System.Collections.Generic;

namespace Pipelining
{
    public class TerminatedPipe<T>
    {
        private readonly SelfPipe<T> _pipe;

        public TerminatedPipe(IGenerator<T> source, IConsumer<T> destination)
        {
            _pipe = new SelfPipe<T>(source, destination);
        }

        public IEnumerable<T> Transform()
        {
            return _pipe.Transform(null);
        }

        public void Start()
        {
            var enumerable = _pipe.Transform(null);
            var enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext()) { }
        }
    }
}
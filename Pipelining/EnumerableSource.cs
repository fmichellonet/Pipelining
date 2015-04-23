using System.Collections.Generic;

namespace Pipelining
{
    public class EnumerableSource<T> : GeneratorBase<T>
    {
        private readonly IEnumerable<T> _enumerable;

        public EnumerableSource(IEnumerable<T> enumerable)
        {
            _enumerable = enumerable;
        }

        public override IEnumerable<T> Generate()
        {
            foreach (var item in _enumerable)
                yield return item;
        }
    }
}
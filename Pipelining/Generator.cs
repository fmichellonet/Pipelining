using System;
using System.Collections.Generic;

namespace Pipelining
{
    public class Generator<T> : GeneratorBase<T>
    {
        private readonly Func<T> _generate;

        public Generator(Func<T> generate)
        {
            _generate = generate;
        }

        public override IEnumerable<T> Generate()
        {
            T val;
            while ((val = _generate.Invoke()) != null)
                yield return val;
        }
    }
}
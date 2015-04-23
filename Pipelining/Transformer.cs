using System;
using System.Collections.Generic;

namespace Pipelining
{
    public class Transformer<TIn, TOut> : TransformerBase<TIn, TOut>
    {

        private readonly Func<TIn, TOut> _singleInSingleOut;
        private readonly Func<TIn, IEnumerable<TOut>> _singleInMultipleOut;

        public Transformer(Func<TIn, TOut> singleInSingleOut)
        {
            _singleInSingleOut = singleInSingleOut;
        }

        public Transformer(Func<TIn, IEnumerable<TOut>> @out)
        {
            _singleInMultipleOut = @out;
        }

        public override IEnumerable<TOut> Transform(IEnumerable<TIn> values)
        {
            foreach (TIn value in values)
            {
                if (_singleInSingleOut != null)
                    yield return _singleInSingleOut.Invoke(value);
                else if (_singleInMultipleOut != null)
                {
                    foreach (TOut item in _singleInMultipleOut.Invoke(value))
                    {
                        yield return item;
                    }
                }
            }
        }
    }
}
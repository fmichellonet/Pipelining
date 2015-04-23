using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipelining
{
    public class Consumer<T> : ConsumerBase<T>
    {
        private readonly Action<T> _consume;

        public Consumer(Action<T> consume)
        {
            _consume = consume;
        }

        public override void Consume(T value)
        {
            _consume.Invoke(value);
        }
    }
}

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pipelining.Tests
{
    public class LetterCount : TransformerBase<string, int>
    {
        public override IEnumerable<int> Transform(IEnumerable<string> values)
        {
            foreach (var item in values)
                yield return item.Length;
        }
    }
}

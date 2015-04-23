using System.Collections.Generic;

namespace Pipelining
{
    public static class ObjectExtensions
    {
        public static IEnumerable<T> AsEnumerable<T>(this T item)
        {
            return new[] { item };
        }
    }
}
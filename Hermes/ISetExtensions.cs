using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hermes.Bnf
{
    public static class ISetExtensions
    {
        public static int UnionWithAddedCount<T>(this ISet<T> set, IEnumerable<T> items)
        {
            int sizeBefore = set.Count;
            set.UnionWith(items);
            return set.Count - sizeBefore;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hermes.Parsers
{
    public class ParseState
        :IEnumerable<Item>
    {
        public readonly Item[] Items;

        public ParseState(IEnumerable<Item> items)
        {
            Items = items.ToArray();
        }

        public override int GetHashCode()
        {
            if (Items.Length == 0)
                return 0;
            return Items.Select(a => a.GetHashCode()).Aggregate((a, b) => a ^ b);
        }

        public override bool Equals(object obj)
        {
            if (obj is ParseState)
                return Equals((ParseState)obj);
            return base.Equals(obj);
        }

        public bool Equals(ParseState other)
        {
            if (Items.Length != other.Items.Length)
                return false;

            return other.Items.Zip(Items, (a, b) => a.Equals(b)).All(a => a);
        }

        public IEnumerator<Item> GetEnumerator()
        {
            foreach (var item in Items)
                yield return item;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public override string ToString()
        {
            return "State " + GetHashCode();
        }
    }
}

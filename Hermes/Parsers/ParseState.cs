using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hermes.Parsers
{
    public class ParseState
        :IEnumerable<Item>
    {
        private readonly int hash;
        public readonly Item[] Items;
        public readonly bool AcceptingState;

        public ParseState(IEnumerable<Item> items, bool accepting)
        {
            AcceptingState = accepting;
            Items = items.ToArray();

            unchecked
            {
                int h = AcceptingState.GetHashCode();
                int c = 0;
                foreach (var item in items)
                {
                    c++;
                    h ^= item.GetHashCode() * (c * 13);
                }
                hash = h;
            }
        }

        public override int GetHashCode()
        {
            return hash;
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
            return "Accepting = " + AcceptingState + ", Count = " + Items.Length;
        }
    }
}

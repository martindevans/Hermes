using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermes.Bnf;

namespace Hermes.Parsers
{
    public struct Item
    {
        public readonly Production Production;
        public readonly int Position;

        public Item(Production p, int position)
        {
            Production = p;
            Position = position;
        }

        public override string ToString()
        {
            int pos = Position;
            return Production.Head.ToString() + " => " + String.Join(" ", Production.Body.Select((a, i) => (i == pos ? "." : "") + a.ToString())) + (Position == Production.Body.Length ? "." : "");
        }

        public override int GetHashCode()
        {
            return Production.GetHashCode() ^ Position;
        }

        public override bool Equals(object obj)
        {
            if (obj is Item)
                return Equals((Item)obj);

            return base.Equals(obj);
        }

        public bool Equals(Item other)
        {
            if (other.Position != Position)
                return false;

            return Production.Equals(other.Production);
        }
    }
}

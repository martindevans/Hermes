using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hermes.Bnf
{
    public struct Production
    {
        public readonly BnfTerm[] Body;
        public readonly NonTerminal Head;

        public Production(NonTerminal head, ConcatenationRule body)
        {
            Body = body.ToArray();
            Head = head;
        }

        public override string ToString()
        {
            return Head.ToString() + " => " + String.Join(",", Body.Select(a => a.ToString()));
        }

        public override int GetHashCode()
        {
            return Head.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Production)
                return Equals((Production)obj);

            return base.Equals(obj);
        }

        public bool Equals(Production other)
        {
            if (!Head.Equals(other.Head))
                return false;

            if (Body.Length != other.Body.Length)
                return false;

            for (int i = 0; i < Body.Length; i++)
                if (!Body[i].Equals(other.Body[i]))
                    return false;

            return true;
        }
    }
}

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
    }
}

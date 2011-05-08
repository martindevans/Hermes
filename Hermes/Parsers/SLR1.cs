using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermes.Bnf;
using Hermes.Tokenising;

namespace Hermes.Parsers
{
    public class SLR1
        : LR0
    {
        public SLR1(Grammar grammar)
            : base(grammar)
        {
        }

        protected override ParserAction Action(ParseState state, Token token, out Production production, out ParseState nextState)
        {
            var lr0 = base.Action(state, token, out production, out nextState);

            if (token != null && lr0 == ParserAction.Reduce && !Grammar.GetFollowSet(production.Head).Contains(token.Terminal))
                return ParserAction.Error;

            return lr0;
        }
    }
}

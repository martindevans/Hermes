using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermes.Bnf;
using System.Text.RegularExpressions;

namespace HermesTests.Grammars
{
    public class WellBalancedBrackets
        :Grammar
    {
        public WellBalancedBrackets(string openBracket, string closeBracket)
            : base(ConstructGrammar(openBracket, closeBracket), GetWhitespace())
        {
        }

        private static Terminal[] GetWhitespace()
        {
            return new Terminal[] { new Terminal("Whitespace", " |\n|\r|\t", true) };
        }

        private static NonTerminal ConstructGrammar(string openBracket, string closeBracket)
        {
            Terminal open = new Terminal(Regex.Escape(openBracket));
            Terminal close = new Terminal(Regex.Escape(closeBracket));

            NonTerminal start = new NonTerminal("start");
            NonTerminal wbbList = new NonTerminal("wbb List");
            NonTerminal openBracketPlusStuff = new NonTerminal("( + L|)");
            NonTerminal stuffPlusClose = new NonTerminal("L + ) | )");

            start.Rules = openBracketPlusStuff + wbbList;

            wbbList.Rules = openBracketPlusStuff + wbbList | "";

            openBracketPlusStuff.Rules = open + stuffPlusClose;

            stuffPlusClose.Rules = wbbList + close | close;

            return start;
        }
    }
}

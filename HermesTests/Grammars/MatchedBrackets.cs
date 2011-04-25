using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hermes.Bnf;
using System.Text.RegularExpressions;

namespace HermesTests.Grammars
{
    public class MatchedBrackets
       : Grammar
    {
        public MatchedBrackets(string openBracket, string closeBracket)
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

            NonTerminal s = new NonTerminal("S");
            s.Rules = open + s + close | "";

            return s;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermes.Bnf;

namespace HermesTests.Grammars
{
    public class MatchedBrackets
        :Grammar
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
            Terminal open = new Terminal(openBracket);
            Terminal close = new Terminal(closeBracket);

            NonTerminal s = new NonTerminal("S");

            s.Rules = s + s | open + s + close | open + close;

            return s;
        }
    }
}
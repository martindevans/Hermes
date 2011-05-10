using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hermes.Bnf;

namespace HermesTests.Grammars
{
    public class CPointerHandling
        :Grammar
    {
        public CPointerHandling()
            :base(ConstructGrammar(), GetWhitespace())
        {
        }

        private static Terminal[] GetWhitespace()
        {
            return new Terminal[] { new Terminal("Whitespace", " |\n|\r|\t", true) };
        }

        private static NonTerminal ConstructGrammar()
        {
            NonTerminal R = new NonTerminal("R");
            NonTerminal S = new NonTerminal("S");
            NonTerminal V = new NonTerminal("V");

            Terminal id = new Terminal("id", @"([A-Za-z]+)([0-9A-Za-z]*)");

            R.Rules = S;
            S.Rules = V + "=" + V;
            V.Rules = id;

            return R;
        }
    }
}

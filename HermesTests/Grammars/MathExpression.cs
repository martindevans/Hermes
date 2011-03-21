using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermes.Bnf;

namespace HermesTests.Grammars
{
    class MathExpression
        : Grammar
    {
        public MathExpression()
            : base(ConstructGrammar(), GetWhitespace())
        {
        }

        private static NonTerminal ConstructGrammar()
        {
            NonTerminal expression = new NonTerminal("expression");
            NonTerminal term = new NonTerminal("term");
            NonTerminal factor = new NonTerminal("factor");

            Terminal number = new Terminal("number", @"[0-9]+(\.[0-9]+)?");

            expression.Rules = expression + "+" + term | expression + "-" + term | term;
            term.Rules       = term + "*" + factor | term + "/" + factor | factor;


            return expression;
        }

        private static Terminal[] GetWhitespace()
        {
            return new Terminal[] { new Terminal("Whitespace", " |\n|\r|\t", true) };
        }
    }
}

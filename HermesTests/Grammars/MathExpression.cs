using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermes.Bnf;
using System.Text.RegularExpressions;

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

            Terminal multiply = new Terminal("*", Regex.Escape("*"));
            Terminal divide = new Terminal("/", Regex.Escape("/"));
            Terminal add = new Terminal("+", Regex.Escape("+"));
            Terminal subtract = new Terminal("-", Regex.Escape("-"));
            Terminal openBracket = new Terminal("(", Regex.Escape("("));
            Terminal closeBracket = new Terminal(")", Regex.Escape(")"));

            expression.Rules = expression + add + term | expression + subtract + term | term;
            term.Rules       = term + multiply + factor | term + divide + factor | factor;
            factor.Rules     = openBracket + expression + closeBracket | number;

            return expression;
        }

        private static Terminal[] GetWhitespace()
        {
            return new Terminal[] { new Terminal("Whitespace", " |\n|\r|\t", true) };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermes.Bnf;

namespace Hermes.Bnf
{
    public static class StringExtensions
    {
        public static Terminal AsTerminal(this string s)
        {
            return new Terminal(s);
        }

        public static NonTerminal Star(this string s)
        {
            return ((RuleAlternation)s).Star();
        }

        public static NonTerminal Optional(this string s)
        {
            return ((RuleAlternation)s).Optional();
        }
    }
}

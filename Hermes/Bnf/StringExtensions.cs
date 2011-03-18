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
    }
}

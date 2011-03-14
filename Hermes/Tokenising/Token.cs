using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermes.Grammar;

namespace Hermes.Tokenising
{
    public class Token
    {
        public readonly Terminal Terminal;
        public readonly string Value;

        public Token(Terminal terminal, string match)
        {
            this.Terminal = terminal;
            this.Value = match;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}

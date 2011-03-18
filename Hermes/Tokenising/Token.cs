using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermes.Bnf;

namespace Hermes.Tokenising
{
    public class Token
    {
        public readonly Terminal Terminal;
        public readonly string Value;
        public readonly int Line;
        public readonly int Column;

        public Token(Terminal terminal, string value, int line, int column)
        {
            this.Terminal = terminal;
            this.Value = value;
            this.Line = line;
            this.Column = column;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermes.Bnf;
using System.IO;

namespace Hermes.Parsers
{
    public class RecursiveDescentParser
        : IParser
    {
        private Grammar g;

        public RecursiveDescentParser(Grammar g)
        {
            this.g = g;
        }

        public ParseTree Parse(Stream input)
        {
            throw new NotImplementedException();
        }
    }
}

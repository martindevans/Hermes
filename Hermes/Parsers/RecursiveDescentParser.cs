using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermes.Bnf;
using System.IO;

namespace Hermes.Parsers
{
    public class RecursiveDescentParser
        : Parser
    {
        private Grammar g;

        public RecursiveDescentParser(Grammar g)
        {
            this.g = g;

            ConstructPredictiveParseTable();
        }

        private void ConstructPredictiveParseTable()
        {
            throw new NotImplementedException();
        }

        public override ParseTree Parse(Stream input)
        {
            var root = Parse(new StreamReader(input), g.Root);

            return new ParseTree(root);
        }

        private ParseTreeNode Parse(StreamReader input, NonTerminal nt)
        {
            throw new NotImplementedException();
            foreach (var rule in nt.Rules)
            {
            }
        }
    }
}

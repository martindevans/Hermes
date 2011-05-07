using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Hermes.Bnf;

namespace Hermes.Parsers
{
    public abstract class Parser
    {
        public Grammar Grammar;

        public Parser(Grammar grammar)
        {
            Grammar = grammar;
        }

        public ParseTree Parse(String input)
        {
            var root = Parse(Grammar.CreateLexer(input), Grammar.Root);

            return new ParseTree(root);
        }

        protected abstract ParseTreeNode Parse(Lexer lexer, NonTerminal root);
    }
}

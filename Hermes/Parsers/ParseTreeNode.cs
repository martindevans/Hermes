using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermes.Bnf;
using Hermes.Tokenising;
using System.Collections.ObjectModel;

namespace Hermes.Parsers
{
    public class ParseTreeNode
    {
        private readonly ParseTreeNode parent;
        private readonly ReadOnlyCollection<ParseTreeNode> children;
        private readonly NonTerminal nonTerminal;
        private readonly Token token;

        public ParseTreeNode Parent
        {
            get { return parent; }
        }

        public ReadOnlyCollection<ParseTreeNode> Children
        {
            get { return children; }
        }

        public NonTerminal NonTerminal
        {
            get { return nonTerminal; }
        }

        public Token Token
        {
            get { return token; }
        }

        public bool IsLeaf
        {
            get { return nonTerminal == null; }
        }

        public ParseTreeNode(ParseTreeNode parent, IEnumerable<ParseTreeNode> children, NonTerminal nonTerminal)
        {
            this.parent = parent;
            this.children = new ReadOnlyCollection<ParseTreeNode>(children.ToArray());
            this.nonTerminal = nonTerminal;
        }

        public ParseTreeNode(ParseTreeNode parent, Token token)
        {
            this.parent = parent;
            this.token = token;
        }
    }
}

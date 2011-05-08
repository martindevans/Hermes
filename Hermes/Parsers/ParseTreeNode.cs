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
        private ParseTreeNode parent;
        private readonly List<ParseTreeNode> children = new List<ParseTreeNode>();
        private readonly NonTerminal nonTerminal;
        private readonly Token token;

        public ParseTreeNode Parent
        {
            get { return parent; }
            set
            {
                if (parent == value)
                    return;

                if (parent != null)
                    parent.children.Remove(this);

                parent = value;

                if (parent != null)
                    parent.children.Add(this);
            }
        }

        public ReadOnlyCollection<ParseTreeNode> Children
        {
            get { return new ReadOnlyCollection<ParseTreeNode>(children); }
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
            get { return children.FirstOrDefault() == null; }
        }

        public ParseTreeNode(NonTerminal nonTerminal)
        {
            this.nonTerminal = nonTerminal;
        }

        public ParseTreeNode(Token token)
        {
            this.token = token;
        }

        public ParseTreeNode(ParseTreeNode parent, NonTerminal nonTerminal)
        {
            this.Parent = parent;
            this.nonTerminal = nonTerminal;
        }

        public ParseTreeNode(ParseTreeNode parent, Token token)
        {
            this.Parent = parent;
            this.token = token;
        }
    }
}

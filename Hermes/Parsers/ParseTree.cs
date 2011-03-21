using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hermes.Parsers
{
    public class ParseTree
    {
        private readonly ParseTreeNode root;

        public ParseTreeNode Root
        {
            get { return root; }
        } 

        public ParseTree(ParseTreeNode root)
        {
            this.root = root;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hermes.Bnf
{
    public class Grammar
    {
        private Terminal[] whitespace;
        public IEnumerable<Terminal> Whitespace
        {
            get { return whitespace; }
        }

        private HashSet<NonTerminal> nonTerminals = new HashSet<NonTerminal>();
        public IEnumerable<NonTerminal> NonTerminals
        {
            get
            {
                return nonTerminals;
            }
        }

        private HashSet<Terminal> terminals = new HashSet<Terminal>();
        public IEnumerable<Terminal> Terminals
        {
            get
            {
                return terminals;
            }
        }

        public Grammar(NonTerminal root, params Terminal[] whitespace)
        {
            this.whitespace = whitespace;
            ConstructGrammar(root);
        }

        private void ConstructGrammar(NonTerminal root)
        {
            foreach (ConcatenationRule option in root.Rules)
            {
                foreach (var term in option)
                {
                    if (term is NonTerminal)
                    {
                        if (nonTerminals.Add(term as NonTerminal))
                            ConstructGrammar(term as NonTerminal);
                    }
                    else
                        terminals.Add(term as Terminal);
                }
            }
        }

        public Lexer CreateLexer(string input)
        {
            var terminals = Terminals.ToList();
            terminals.AddRange(whitespace);

            return new Lexer(input, terminals);
        }
    }
}

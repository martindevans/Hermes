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

        public IEnumerable<Production> Productions
        {
            get
            {
                foreach (var nt in NonTerminals)
                    foreach (var rule in nt.Rules)
                        yield return new Production(nt, rule);
            }
        }

        public readonly NonTerminal Root;

        private Dictionary<BnfTerm, ISet<Terminal>> firstSymbol = new Dictionary<BnfTerm, ISet<Terminal>>();
        private Dictionary<NonTerminal, ISet<Terminal>> follow = new Dictionary<NonTerminal, ISet<Terminal>>();

        #region constructor
        public Grammar(NonTerminal root, params Terminal[] whitespace)
        {
            this.Root = root;
            this.whitespace = whitespace;

            ConstructGrammar(root);
            CalculateFirstSymbols();
        }
        #endregion

        #region setup
        private void ConstructGrammar(NonTerminal root)
        {
            nonTerminals.Add(root);

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

        private void CalculateFirstSymbols()
        {
            foreach (var terminal in Terminals)
            {
                firstSymbol[terminal] = new HashSet<Terminal>();
                if (!terminal.IsIgnored && !terminal.Equals(Terminal.Empty))
                    firstSymbol[terminal].Add(terminal);
            }

            foreach (var nt in NonTerminals)
                firstSymbol[nt] = new HashSet<Terminal>();

            bool change = false;
            do
            {
                change = false;
                foreach (var production in Productions)
                {
                    for (int i = 0; i < production.Body.Length; i++)
                    {
                        var firstOfTerm = firstSymbol[production.Body[i]];
                        var set = firstSymbol[production.Head];

                        int before = set.Count;
                        set.UnionWith(firstOfTerm);
                        if (before < set.Count)
                            change = true;

                        if (!production.Body[i].IsNullable())
                            break;
                    }
                }
            }
            while (change);
        }

        private void CalculateFollowSets()
        {
            foreach (var nt in NonTerminals)
                follow[nt] = new HashSet<Terminal>();

            bool changed = false;
            do
            {
                changed = false;

                foreach (var production in Productions)
                {
                    for (int i = 0; i < production.Body.Length; i++)
                    {
                        for (int j = i + 1; j < production.Body.Length; j++)
                        {
                            
                        }
                    }
                }

            } while (changed);
        }
        #endregion

        public Lexer CreateLexer(string input)
        {
            var terminals = Terminals.ToList();
            terminals.AddRange(whitespace);

            return new Lexer(input, terminals);
        }

        #region first
        public ISet<Terminal> GetFirstSet(params BnfTerm[] terms)
        {
            return GetFirstSet(terms as IEnumerable<BnfTerm>);
        }

        public ISet<Terminal> GetFirstSet(IEnumerable<BnfTerm> terms)
        {
            var first = terms.FirstOrDefault();

            if (first == null)
                return new HashSet<Terminal>();

            ISet<Terminal> result = new HashSet<Terminal>();
            result.UnionWith(firstSymbol[first]);

            if (first.IsNullable())
                result.UnionWith(GetFirstSet(terms.Skip(1)));

            return result;
        }
        #endregion

        #region follow
        public ISet<Terminal> GetFollowSet(NonTerminal a)
        {

        }
        #endregion
    }
}

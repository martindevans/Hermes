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
            CalculateNullable();
            CalculateFirstSymbols();
            CalculateFollowSets();
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

        private void CalculateNullable()
        {
            bool changed = false;
            do
            {
                changed = false;

                foreach (var production in Productions)
                {
                    if (production.Head.IsNullable)
                        continue;

                    if (production.Body.All(a => a.IsNullable))
                    {
                        production.Head.IsNullable = true;
                        changed = true;
                    }
                }
            } while (changed);
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

                        change = firstSymbol[production.Head].UnionWithAddedCount(firstOfTerm) != 0;

                        if (!production.Body[i].IsNullable)
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
                        NonTerminal nt = production.Body[i] as NonTerminal;
                        if (nt == null)
                            continue;

                        var set = follow[nt];

                        for (int j = production.Body.Length - 1; j >= i; j--)
                        {
                            //found a nonterminal midway through a production
                            //if it's followed only by nullable terms, the follow of the entire production is in the follow of this non terminal
                            changed = set.UnionWithAddedCount(follow[production.Head]) != 0;

                            //working backwards, break as soon as we find a non nullable term
                            if (!production.Body[j].IsNullable)
                                break;
                        }

                        //keep adding symbols to the follow set until we find a non nullable term partway through the production
                        for (int j = i + 1; j < production.Body.Length; j++)
                        {
                            changed = follow[nt].UnionWithAddedCount(firstSymbol[production.Body[j]]) != 0;

                            if (!production.Body[j].IsNullable)
                                break;
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

            if (first.IsNullable)
                result.UnionWith(GetFirstSet(terms.Skip(1)));

            return result;
        }
        #endregion

        #region follow
        public ISet<Terminal> GetFollowSet(NonTerminal a)
        {
            return follow[a];
        }
        #endregion
    }
}

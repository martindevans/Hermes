using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermes.Bnf;
using Hermes.Tokenising;

namespace Hermes.Parsers
{
    public class LR0
        :LRParserBase
    {
        public LR0(Grammar grammar)
            :base(grammar)
        {
        }

        public static Automaton CreateAutomaton(Grammar g)
        {
            //initialise to closure of start item
            HashSet<ParseState> states = new HashSet<ParseState>();
            states.Add(new[] { new Item(g.Productions.Where(a => a.Head.Equals(g.Root)).First(), 0) }.Closure(g));

            HashSet<ParseStateTransition> transitions = new HashSet<ParseStateTransition>();

            HashSet<ParseState> sToAdd = new HashSet<ParseState>();
            HashSet<ParseStateTransition> tToAdd = new HashSet<ParseStateTransition>();

            do
            {
                sToAdd.Clear();
                tToAdd.Clear();

                foreach (var state in states)
                {
                    foreach (var item in state)
                    {
                        if (item.Production.Body.Length == item.Position)
                            continue;

                        BnfTerm term = item.Production.Body[item.Position];
                        ParseState j = state.Goto(term, g);

                        sToAdd.Add(j);
                        tToAdd.Add(new ParseStateTransition(state, term, j));
                    }
                }
            }
            while (states.UnionWithAddedCount(sToAdd) != 0 | transitions.UnionWithAddedCount(tToAdd) != 0);

            return new Automaton(transitions);
        }

        protected override KeyValuePair<ParserAction, int> Action(int state, Token token)
        {
            throw new NotImplementedException();
        }

        protected override int Goto(int state, NonTerminal nonTerminal)
        {
            throw new NotImplementedException();
        }

        protected override Production GetProduction(int index)
        {
            throw new NotImplementedException();
        }
    }
}

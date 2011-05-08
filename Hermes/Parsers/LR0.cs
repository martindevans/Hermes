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
        Automaton automaton;

        public LR0(Grammar grammar)
            :base(grammar)
        {
            automaton = CreateAutomaton(grammar);
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

        protected override ParserAction Action(ParseState state, Token token, out Production production, out ParseState nextState)
        {
            if (token != null)
            {
                nextState = automaton[state, token.Terminal];
                if (nextState != null)
                {
                    production = default(Production);
                    return ParserAction.Shift;
                }
            }

            if (state.Count() == 1 && state.First().Position == state.First().Production.Body.Length)
            {
                nextState = null;
                production = state.First().Production;
                return ParserAction.Reduce;
            }

            if (state.AcceptingState)
            {
                production = default(Production);
                nextState = null;
                return ParserAction.Accept;
            }
            else
                throw new ParseException("Unexpected EOF");

            throw new ParseException(String.Format("Unexpected {0} at line {1} column {2}", token.Value, token.Line, token.Column));
        }

        protected override ParseState Goto(ParseState state, NonTerminal nonTerminal)
        {
            return automaton[state, nonTerminal];
        }
    }
}

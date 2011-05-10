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
            states.Add(g.Productions.Where(a => a.Head.Equals(g.Root)).Select(a => new Item(a, 0)).Closure(g));

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

            return new Automaton(transitions, g);
        }

        protected override ParserAction Action(ParseState state, Token token, out Production production, out ParseState nextState)
        {
            bool shift = false;
            bool reduce = false;

            if (token != null)
            {
                nextState = automaton[state, token.Terminal];
                if (nextState != null)
                {
                    production = default(Production);
                    shift = true;
                }
            }
            else
                nextState = null;

            if (state.Count() == 1 && state.First().Position == state.First().Production.Body.Length)
            {
                nextState = null;
                production = state.First().Production;
                reduce = true;
            }
            else
                production = default(Production);

            if (shift && reduce)
                throw new ParseException(String.Format("Shift/Reduce conflict between shift {0} and reduce {1}", nextState, production));
            else if (shift)
                return ParserAction.Shift;
            else if (state.AcceptingState)
                return ParserAction.Accept;
            else if (reduce)
                return ParserAction.Reduce;
            else if (token == null)
                throw new ParseException("Unexpected EOF");

            throw new ParseException(String.Format("Unexpected {0} at line {1} column {2}", token.Value, token.Line, token.Column));
        }

        protected override ParseState Goto(ParseState state, NonTerminal nonTerminal)
        {
            return automaton[state, nonTerminal];
        }
    }
}

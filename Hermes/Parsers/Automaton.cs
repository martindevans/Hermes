using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermes.Bnf;
using Hermes.Tokenising;

namespace Hermes.Parsers
{
    public class Automaton
    {
        HashSet<ParseState> allStates = new HashSet<ParseState>();
        Dictionary<ParseState, Dictionary<BnfTerm, ParseState>> transitionRules = new Dictionary<ParseState, Dictionary<BnfTerm, ParseState>>();

        public Automaton(IEnumerable<ParseStateTransition> transitions)
        {
            foreach (var transition in transitions)
            {
                allStates.Add(transition.Start);
                allStates.Add(transition.End);

                Dictionary<BnfTerm, ParseState> d;
                if (!transitionRules.TryGetValue(transition.Start, out d))
                {
                    d = new Dictionary<BnfTerm, ParseState>();
                    transitionRules.Add(transition.Start, d);
                }

                if (d.ContainsKey(transition.Trigger))
                    throw new InvalidOperationException("Duplicate transition");

                d[transition.Trigger] = transition.End;
            }
        }

        public ParseState this[ParseState current, BnfTerm symbol]
        {
            get
            {
                Dictionary<BnfTerm, ParseState> transition;
                if (!transitionRules.TryGetValue(current, out transition))
                {
                    if (!allStates.Contains(current))
                        throw new ParseException("No such state");
                    else
                        return null;
                }

                ParseState next;
                if (!transition.TryGetValue(symbol, out next))
                    throw new ParseException("Invalid symbol for this state");

                return next;
            }
        }

        public IEnumerable<ParseState> States
        {
            get
            {
                return allStates;
            }
        }

        public IEnumerable<Terminal> Terminals
        {
            get
            {
                return transitionRules.Values.SelectMany(a => a.Keys)
                    .Distinct()
                    .Where(a => a is Terminal)
                    .Cast<Terminal>();
            }
        }
    }
}

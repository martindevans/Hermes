using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermes.Bnf;

namespace Hermes.Parsers
{
    public class Automaton
    {
        Dictionary<ParseState, Dictionary<BnfTerm, ParseState>> transitionRules = new Dictionary<ParseState, Dictionary<BnfTerm, ParseState>>();

        public Automaton(IEnumerable<ParseStateTransition> transitions)
        {
            foreach (var transition in transitions)
            {
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
                    throw new KeyNotFoundException("No such state");

                ParseState next;
                if (!transition.TryGetValue(symbol, out next))
                    throw new KeyNotFoundException("Invalid symbol for this state");

                return next;
            }
        }

        public IEnumerable<ParseState> States
        {
            get
            {
                return transitionRules.Keys.Union(transitionRules.Values.SelectMany(a => a.Values), EqualityComparer<ParseState>.Default);
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

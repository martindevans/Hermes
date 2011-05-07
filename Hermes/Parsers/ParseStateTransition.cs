using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermes.Bnf;

namespace Hermes.Parsers
{
    public class ParseStateTransition
    {
        public readonly ParseState Start;
        public readonly BnfTerm Trigger;
        public readonly ParseState End;

        public ParseStateTransition(ParseState start, BnfTerm trigger, ParseState end)
        {
            Start = start;
            Trigger = trigger;
            End = end;
        }

        public override int GetHashCode()
        {
            return Trigger.GetHashCode() ^ Start.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is ParseStateTransition)
                return Equals(obj as ParseStateTransition);

            return base.Equals(obj);
        }

        public bool Equals(ParseStateTransition other)
        {
            return Start.Equals(other.Start) && Trigger.Equals(other.Trigger) && End.Equals(other.End);
        }
    }
}

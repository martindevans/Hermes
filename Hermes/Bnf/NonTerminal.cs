using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hermes.Bnf
{
    public class NonTerminal
        :BnfTerm
    {
        public RuleAlternation Rules;

        public NonTerminal(string name, ConcatenationRule rule = null)
            :base(name)
        {
            Rules = rule;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return object.ReferenceEquals(obj, this);
        }

        public static implicit operator RuleAlternation(NonTerminal nonTerminal)
        {
            return new RuleAlternation(nonTerminal);
        }

        public NonTerminal Star()
        {
            return ((RuleAlternation)this).Star();
        }

        public NonTerminal Optional()
        {
            return ((RuleAlternation)this).Optional();
        }
    }
}

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

        protected override bool CalculateIsNullable()
        {
            foreach (ConcatenationRule rule in Rules)
            {
                if (rule.IsNullable())
                    return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            return object.ReferenceEquals(obj, this);
        }
    }
}

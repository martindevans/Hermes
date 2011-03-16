using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hermes.Grammar
{
    public class RuleAlternation
    {
        HashSet<ConcatenationRule> options = new HashSet<ConcatenationRule>();

        public RuleAlternation(ConcatenationRule rule)
        {
            options.Add(rule);
        }

        public RuleAlternation(ConcatenationRule a, ConcatenationRule b)
        {
            options.Add(a);
            options.Add(b);
        }

        public RuleAlternation(IEnumerable<ConcatenationRule> a, ConcatenationRule b)
        {
            options.UnionWith(a);
            options.Add(b);
        }

        #region operators
        public static RuleAlternation operator |(RuleAlternation left, ConcatenationRule right)
        {
            return new RuleAlternation(left.options, right);
        }

        public static implicit operator RuleAlternation(Terminal terminal)
        {
            return new RuleAlternation(terminal);
        }
        #endregion
    }
}

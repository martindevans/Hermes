using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hermes.Grammar
{
    public class ConcatenationRule
    {
        List<BnfTerm> terms = new List<BnfTerm>();

        public ConcatenationRule(BnfTerm terminal)
        {
            terms.Add(terminal);
        }

        public ConcatenationRule(ConcatenationRule left, ConcatenationRule right)
        {
            terms.AddRange(left.terms);
            terms.AddRange(right.terms);
        }

        public override int GetHashCode()
        {
            int hash = 0;

            for (int i = 0; i < terms.Count; i++)
                hash ^= terms[i].GetHashCode();

            return hash;
        }

        public override bool Equals(object obj)
        {
            ConcatenationRule other = obj as ConcatenationRule;
            if (other == null)
                return false;

            if (terms.Count != other.terms.Count)
                return false;

            for (int i = 0; i < terms.Count; i++)
                if (!terms[i].Equals(other.terms[i]))
                    return false;

            return true;
        }

        #region operators
        public static ConcatenationRule operator +(ConcatenationRule left, ConcatenationRule right)
        {
            return new ConcatenationRule(left, right);
        }

        public static implicit operator RuleAlternation(ConcatenationRule r)
        {
            return new RuleAlternation(r);
        }

        public static implicit operator ConcatenationRule(string regex)
        {
            return new Terminal(regex);
        }

        public static implicit operator ConcatenationRule(Terminal terminal)
        {
            return new ConcatenationRule(terminal);
        }
        #endregion
    }
}

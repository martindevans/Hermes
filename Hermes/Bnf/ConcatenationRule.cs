using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hermes.Bnf
{
    public class ConcatenationRule
        :IEnumerable<BnfTerm>
    {
        List<BnfTerm> terms = new List<BnfTerm>();

        public int Count
        {
            get
            {
                return terms.Count;
            }
        }

        internal ConcatenationRule(BnfTerm terminal)
        {
            terms.Add(terminal);
        }

        internal ConcatenationRule(ConcatenationRule left, ConcatenationRule right)
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

        public bool IsNullable()
        {
            foreach (var term in this)
            {
                if (!term.IsNullable)
                    return false;
            }

            return true;
        }

        #region operators
        public static ConcatenationRule operator +(ConcatenationRule left, ConcatenationRule right)
        {
            return new ConcatenationRule(left, right);
        }

        public static RuleAlternation operator |(string left, ConcatenationRule right)
        {
            return new RuleAlternation(new Terminal(Regex.Escape(left)), right);
        }

        public static RuleAlternation operator |(ConcatenationRule left, ConcatenationRule right)
        {
            return new RuleAlternation(left, right);
        }

        public static implicit operator RuleAlternation(ConcatenationRule r)
        {
            return new RuleAlternation(r);
        }

        public static implicit operator ConcatenationRule(string value)
        {
            return new Terminal(Regex.Escape(value));
        }

        public static implicit operator ConcatenationRule(Terminal terminal)
        {
            return new ConcatenationRule(terminal);
        }
        #endregion

        public IEnumerator<BnfTerm> GetEnumerator()
        {
            return terms.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

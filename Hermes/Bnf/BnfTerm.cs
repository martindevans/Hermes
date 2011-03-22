using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hermes.Bnf
{
    public abstract class BnfTerm
    {
        public readonly string Name;

        public BnfTerm(string name)
        {
            Name = name;
        }

        public static implicit operator ConcatenationRule(BnfTerm t)
        {
            return new ConcatenationRule(t);
        }

        public static ConcatenationRule Plus(BnfTerm left, BnfTerm right)
        {
            return new ConcatenationRule(left, right);
        }

        public static ConcatenationRule operator +(BnfTerm left, BnfTerm right)
        {
            return new ConcatenationRule(left, right);
        }

        public static implicit operator BnfTerm(string regex)
        {
            return new Terminal(regex);
        }

        public static RuleAlternation operator |(BnfTerm left, BnfTerm right)
        {
            return new RuleAlternation(left, right);
        }

        public abstract override bool Equals(object obj);

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        #region nullable
        bool? nullable = null;
        public bool IsNullable()
        {
            if (!nullable.HasValue)
                nullable = CalculateIsNullable();

            return nullable.Value;
        }

        protected abstract bool CalculateIsNullable();
        #endregion
    }
}

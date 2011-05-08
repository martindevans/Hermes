using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hermes.Bnf
{
    /// <summary>
    /// 
    /// </summary>
    public class Terminal
        : BnfTerm
    {
        public readonly Regex Regex;
        public readonly bool IsIgnored = false;

        public static readonly Terminal Empty = new Terminal("");

        bool nullable;
        public override bool IsNullable
        {
            get
            {
                return nullable;
            }
            internal set{}
        }

        #region constructors
        public Terminal(string regex, bool isIgnored = false)
            : this(regex, regex, isIgnored)
        {
        }

        public Terminal(Regex regex, bool isIgnored = false)
            : this(regex.ToString(), regex, isIgnored)
        {
        }

        public Terminal(string name, string regex, bool isIgnored = false)
            : this(name, new Regex(regex), isIgnored)
        {
        }

        public Terminal(string name, Regex regex, bool isIgnored = false)
            : base(name)
        {
            this.Regex = regex;
            this.IsIgnored = isIgnored;
            this.nullable = Regex.IsMatch("") || IsIgnored;
        }
        #endregion

        public bool Match(string input, int startIndex, out string match)
        {
            var m = Regex.Match(input, startIndex);

            if (!m.Success || m.Index != startIndex)
            {
                match = null;
                return false;
            }

            match = m.Value;
            return true;
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static implicit operator Terminal(string regex)
        {
            return new Terminal(regex);
        }

        public override bool Equals(object obj)
        {
            Terminal other = obj as Terminal;
            if (object.ReferenceEquals(other, null))
                return false;

            return Regex.ToString().Equals(other.Regex.ToString());
        }

        public static bool operator ==(Terminal a, Terminal b)
        {
            if (object.ReferenceEquals(a, null))
            {
                if (object.ReferenceEquals(b, null))
                    return true;
                else
                    return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Terminal a, Terminal b)
        {
            if (object.ReferenceEquals(a, null))
            {
                if (object.ReferenceEquals(b, null))
                    return false;
                else
                    return true;
            }

            return !a.Equals(b);
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

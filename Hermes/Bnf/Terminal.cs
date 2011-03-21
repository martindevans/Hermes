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

        #region constructors
        public Terminal(string regex, bool isIgnored=false)
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

        public Terminal(string name, Regex regex, bool isIgnored=false)
            :base(name)
        {
            this.Regex = regex;
            this.IsIgnored = isIgnored;
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

        public static implicit operator Terminal(string regex)
        {
            return new Terminal(regex);
        }

        public override bool Equals(object obj)
        {
            Terminal other = obj as Terminal;
            if (other == null)
                return false;

            return Regex.Equals(other.Regex);
        }
    }
}

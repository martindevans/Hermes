using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hermes.Grammar
{
    /// <summary>
    /// 
    /// </summary>
    public class Terminal
    {
        public readonly Regex regex;
        public readonly string Name;

        public Terminal(string regex)
            : this(regex, regex)
        {
        }

        public Terminal(string name, string regex)
            :this(name, new Regex(regex))
        {
        }

        public Terminal(string name, Regex regex)
        {
            this.regex = regex;
            this.Name = name;
        }

        public bool Match(string input, out string match)
        {
            var m = regex.Match(input);

            if (!m.Success || m.Index != 0)
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

        public static Rule Plus(Terminal left, Terminal right)
        {
            return null;
        }

        public static Rule Plus(Terminal left, string right)
        {
            return null;
        }

        public static Rule Plus(string left, Terminal right)
        {
            return null;
        }

        public static Rule operator +(Terminal left, Terminal right)
        {
            return Terminal.Plus(left, right);
        }

        public static Rule operator +(Terminal left, string right)
        {
            return Terminal.Plus(left, right);
        }

        public static Rule operator +(string left, Terminal right)
        {
            return Terminal.Plus(left, right);
        }
    }
}

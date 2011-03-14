using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hermes.Grammar
{
    public class NonTerminal
    {
        public readonly string Name;

        public NonTerminal(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public static Rule Plus(NonTerminal left, NonTerminal right)
        {
            return null;
        }

        public static Rule Plus(NonTerminal left, Terminal right)
        {
            return null;
        }

        public static Rule Plus(Terminal left, NonTerminal right)
        {
            return null;
        }

        public static Rule Plus(NonTerminal left, string right)
        {
            var terminal = new Terminal(right);
            return NonTerminal.Plus(left, terminal);
        }

        public static Rule Plus(string left, NonTerminal right)
        {
            var termainal = new Terminal(left);
            return NonTerminal.Plus(termainal, right);
        }

        #region operators
        public static Rule operator +(NonTerminal left, NonTerminal right)
        {
            return NonTerminal.Plus(left, right);
        }

        public static Rule operator +(NonTerminal left, Terminal right)
        {
            return NonTerminal.Plus(left, right);
        }

        public static Rule operator +(Terminal left, NonTerminal right)
        {
            return NonTerminal.Plus(left, right);
        }

        public static Rule operator +(NonTerminal left, string right)
        {
            return NonTerminal.Plus(left, right);
        }

        public static Rule operator +(string left, NonTerminal right)
        {
            return NonTerminal.Plus(left, right);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hermes.Bnf
{
    public class RuleAlternation
        :IEnumerable<ConcatenationRule>
    {
        HashSet<ConcatenationRule> options = new HashSet<ConcatenationRule>();

        public int Count
        {
            get
            {
                return options.Count;
            }
        }

        internal RuleAlternation(ConcatenationRule rule)
        {
            options.Add(rule);
        }

        internal RuleAlternation(ConcatenationRule a, ConcatenationRule b)
        {
            options.Add(a);
            options.Add(b);
        }

        internal RuleAlternation(IEnumerable<ConcatenationRule> a, ConcatenationRule b)
        {
            options.UnionWith(a);
            options.Add(b);
        }

        public override bool Equals(object obj)
        {
            RuleAlternation a = obj as RuleAlternation;
            if (a == null)
                return false;

            if (a.Count != this.Count)
                return false;

            return a.Zip(this, (x, y) => x.Equals(y)).Aggregate((x, y) => x && y);
        }

        public NonTerminal Star()
        {
            NonTerminal alt = new NonTerminal("alt");
            alt.Rules = this;

            NonTerminal listTail = new NonTerminal("listTail");
            listTail.Rules = alt | "";

            NonTerminal list = new NonTerminal("list");
            list.Rules = alt + listTail | "".AsTerminal();

            return list;
        }

        public NonTerminal Optional()
        {
            NonTerminal alt = new NonTerminal("alt");
            alt.Rules = this;

            NonTerminal opt = new NonTerminal("optional");
            opt.Rules = alt + "".AsTerminal();

            return opt;
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

        public static implicit operator RuleAlternation(string regex)
        {
            return new RuleAlternation(new Terminal(Regex.Escape(regex)));
        }
        #endregion

        public IEnumerator<ConcatenationRule> GetEnumerator()
        {
            return options.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hermes.Bnf;
using Hermes;

namespace HermesTests
{
    [TestClass]
    public class GrammarTest
    {
        [TestMethod]
        public void ConstructAGrammar()
        {
            NonTerminal op = new NonTerminal("Operator");
            NonTerminal expr = new NonTerminal("Expression");

            Terminal number = new Terminal("Number", @"\b\d+\b");
            Terminal plus = new Terminal("Plus", @"\+");
            Terminal multiply = @"\*";

            op.Rules = plus | multiply;
            expr.Rules = number + op + expr;

            Terminal whitespace = new Terminal("Whitespace", " |\n|\r", true);
            Grammar g = new Grammar(expr, whitespace);

            Assert.AreEqual(3, g.Terminals.Count());
            Assert.AreEqual(3, g.NonTerminals.Count()); //Although the grammar only specifies 2 Nonterminals, grammar creates 1 internally for the root

            Lexer lexer = g.CreateLexer("1 + 2 * 3");

            Assert.AreEqual(5, lexer.Count());
        }

        [TestMethod]
        public void GetFirstSet()
        {
            NonTerminal a = new NonTerminal("A");
            NonTerminal b = new NonTerminal("B");
            NonTerminal c = new NonTerminal("C");
            NonTerminal d = new NonTerminal("D");

            a.Rules = b + d | c + d;
            b.Rules = "b";
            c.Rules = "c";
            d.Rules = "d";

            Grammar g = new Grammar(a);
            var set = g.GetFirstSet(a);

            Assert.IsNotNull(set);
            Assert.AreEqual(2, set.Count);
            Assert.IsFalse(set.Contains("d"));
            Assert.IsTrue(set.Contains("b"));
            Assert.IsTrue(set.Contains("c"));
        }

        [TestMethod]
        public void GetFirstSetWithEmptyStrings()
        {
            NonTerminal a = new NonTerminal("A");
            NonTerminal b = new NonTerminal("B");

            a.Rules = b + "s";
            b.Rules = "".AsTerminal() | "x";

            Grammar g = new Grammar(a);
            var set = g.GetFirstSet(a);

            Assert.IsNotNull(set);
            Assert.AreEqual(2, set.Count);
            Assert.IsTrue(set.Contains("x"));
            Assert.IsTrue(set.Contains("s"));
        }

        [TestMethod]
        public void IsNullable()
        {
            NonTerminal a = new NonTerminal("A");
            NonTerminal b = new NonTerminal("B");
            NonTerminal c = new NonTerminal("C");

            a.Rules = b + "s";
            b.Rules = "".AsTerminal() | c;
            c.Rules = "c";

            Grammar g = new Grammar(a);

            Assert.IsTrue(b.IsNullable);
            Assert.IsFalse(a.IsNullable);
            Assert.IsFalse(c.IsNullable);
        }

        [TestMethod]
        public void GetFollowSet()
        {
            NonTerminal a = new NonTerminal("A");
            NonTerminal b = new NonTerminal("B");

            a.Rules = "a" + b;
            b.Rules = b + "a" | "c";

            Grammar g = new Grammar(a);

            var followA = g.GetFollowSet(a);
            Assert.AreEqual(0, followA.Count);

            var followB = g.GetFollowSet(b);
            Assert.AreEqual(1, followB.Count);
            Assert.IsTrue(followB.Contains("a"));
        }
    }
}

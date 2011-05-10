using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hermes.Bnf;
using Hermes.Parsers;
using HermesTests.Grammars;

namespace HermesTests
{
    [TestClass]
    public class LR0Test
    {
        public static Grammar ConstructTestGrammar(out NonTerminal S, out NonTerminal T, out NonTerminal F)
        {
            T = new NonTerminal("T");
            F = new NonTerminal("F");

            T.Rules = T + "*" + F | F;
            F.Rules = "(" + T + ")" | new Terminal("ID", "([A-Z]|[a-z])+");

            var g = new Grammar(T, new Terminal(" ", isIgnored:true));

            S = g.Root;

            return g;
        }

        [TestMethod]
        public void ConstructAutomaton()
        {
            NonTerminal S, T, F;
            Grammar g = ConstructTestGrammar(out S, out T, out F);

            Automaton a = LR0.CreateAutomaton(g);

            Assert.AreEqual(4, a.Terminals.Count());
            Assert.AreEqual(9, a.States.Count());
        }

        [TestMethod]
        public void ParseValidString()
        {
            NonTerminal S, T, F;
            LR0 parser = new LR0(ConstructTestGrammar(out S, out T, out F));

            ParseTree tree = parser.Parse("((x) * y) * foo");

            Assert.IsNotNull(tree);

            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(3, tree.Root.Children.Count);

            Assert.IsNotNull(tree.Root.Children[0].NonTerminal);
            Assert.AreEqual("*", tree.Root.Children[1].Token.Value);
            Assert.IsNotNull(tree.Root.Children[2].NonTerminal);

            Assert.AreEqual("foo", tree.Root.Children[2].Children[0].Token.Value);
        }

        [TestMethod]
        public void ParseMatchedBrackets()
        {
            var lr0 = new LR0(new MatchedBrackets("(", ")"));

            lr0.Parse("()");
        }

        [TestMethod]
        public void ParseMathExpression()
        {
            var lr0 = new LR0(new MathExpression());

            lr0.Parse("1 + 2");
        }

        [TestMethod]
        [ExpectedException(typeof(ParseException))]
        public void ParseInvalidEarlyTerminatingString()
        {
            NonTerminal S, T, F;
            LR0 parser = new LR0(ConstructTestGrammar(out S, out T, out F));

            var tree = parser.Parse("((x)*t)*");
        }

        [TestMethod]
        [ExpectedException(typeof(ParseException))]
        public void ParseInvalidUnexpectedCharacterString()
        {
            NonTerminal S, T, F;
            LR0 parser = new LR0(ConstructTestGrammar(out S, out T, out F));

            var tree = parser.Parse("((x)*t)**");
        }
    }
}

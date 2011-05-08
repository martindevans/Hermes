using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hermes.Bnf;
using Hermes.Parsers;

namespace HermesTests
{
    [TestClass]
    public class LR0Test
    {
        private Grammar ConstructTestGrammar(out NonTerminal S, out NonTerminal T, out NonTerminal F)
        {
            S = new NonTerminal("S");
            T = new NonTerminal("T");
            F = new NonTerminal("F");

            S.Rules = T;
            T.Rules = T + "*" + F | F;
            F.Rules = "(" + T + ")" | new Terminal("ID", "([A-Z]|[a-z])+");

            return new Grammar(S, new Terminal(" ", isIgnored:true));
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

            ParseTree tree = parser.Parse("((x) * t) * foo");

            Assert.IsNotNull(tree);

            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(1, tree.Root.Children.Count);

            var t1 = tree.Root.Children.First();                // T ::= T * F          ((x) * t) * foo
            Assert.AreEqual(3, t1.Children.Count);
            Assert.AreEqual(T, t1.Children[0]);
            Assert.AreEqual("*", t1.Children[1].Token.Value);
            Assert.AreEqual(F, t1.Children[2]);

            var f1 = t1.Children[2];                            // T ::= F              foo
            Assert.AreEqual(1, f1.Children.Count);              // F ::= ID             foo
            Assert.AreEqual("foo", f1.Children[0].Token);       // ID                   foo

            var t2 = t1.Children[0];                            // T ::= T * F          ((x) * t)
            Assert.AreEqual(3, t2.Children.Count);
            Assert.AreEqual(T, t2.Children[0]);
            Assert.AreEqual("*", t2.Children[1].Token.Value);
            Assert.AreEqual(F, t2.Children[2]);
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

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hermes.Bnf;
using HermesTests.Grammars;
using System.Text.RegularExpressions;
using Hermes.Parsers;

namespace HermesTests
{
    [TestClass]
    public class ItemTest
    {
        [TestMethod]
        public void Equals()
        {
            MathExpression grammar = new MathExpression();

            var prodA = grammar.Productions.First();
            var prodA2 = grammar.Productions.First();
            var prodB = grammar.Productions.Skip(1).First();

            Assert.IsTrue(prodA.Equals(prodA2));
            Assert.IsFalse(prodA.Equals(prodB));

            Item itemA = new Item(prodA, 0);
            Item itemA2 = new Item(prodA, 0);
            Item itemB = new Item(prodB, 0);

            Assert.IsTrue(itemA.Equals(itemA2));
            Assert.IsFalse(itemA.Equals(itemB));
            Assert.IsFalse(itemA2.Equals(itemB));
        }

        private IEnumerable<Item> CalculateInitialState(out Grammar g, out NonTerminal S, out NonTerminal T, out NonTerminal F)
        {
            S = new NonTerminal("S");
            var sCopy = S;
            T = new NonTerminal("T");
            F = new NonTerminal("F");

            S.Rules = T + "$";
            T.Rules = T + "*" + F | F;
            F.Rules = "(" + T + ")" | new Terminal("ID", "([A-Z]|[a-z])+");

            g = new Grammar(S, new Terminal(" "));

            Item start = new Item(g.Productions.Where(a => a.Head.Equals(sCopy)).First(), 0);

            return new[] { start }.Closure(g);
        }

        [TestMethod]
        public void Closure()
        {
            NonTerminal S;
            NonTerminal T;
            NonTerminal F;
            Grammar g;
            var initialState = CalculateInitialState(out g, out S, out T, out F);

            Assert.AreEqual(5, initialState.Count());
            Assert.IsTrue(initialState.All(a => a.Position == 0));

            Assert.AreEqual(2, initialState.Where(a => a.Production.Body[0].Equals(T)).Count());
            Assert.AreEqual(1, initialState.Where(a => a.Production.Body[0].Equals(F)).Count());
        }

        [TestMethod]
        public void Goto()
        {
            NonTerminal S;
            NonTerminal T;
            NonTerminal F;
            Grammar g;
            var initialState = CalculateInitialState(out g, out S, out T, out F);

            var newState = initialState.Goto(new Terminal(Regex.Escape("(")), g);

            Assert.AreEqual(5, newState.Count());
        }
    }
}

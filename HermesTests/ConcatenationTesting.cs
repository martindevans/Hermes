using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hermes.Bnf;

namespace HermesTests
{
    [TestClass]
    public class ConcatenationTesting
    {
        [TestMethod]
        public void ConcatenationOfTypedTerminals()
        {
            Terminal a = new Terminal("a");
            Terminal b = new Terminal("b");

            var c = a + b;

            Assert.IsNotNull(c);
            Assert.AreEqual(2, c.Count);
            Assert.IsInstanceOfType(c, typeof(ConcatenationRule));
        }

        [TestMethod]
        public void ConcatenationOfTypedNonTerminals()
        {
            NonTerminal a = new NonTerminal("a");
            NonTerminal b = new NonTerminal("b");

            var c = a + b;

            Assert.IsNotNull(c);
            Assert.AreEqual(2, c.Count);
            Assert.IsInstanceOfType(c, typeof(ConcatenationRule));
        }

        [TestMethod]
        public void ConcatenationOfUntypedTerminals()
        {
            string a = "a";
            string b = "b";

            ConcatenationRule c = a + b;

            Assert.IsNotNull(c);
            Assert.AreEqual(1, c.Count);
        }

        [TestMethod]
        public void ConcatenationOfNonTerminalAndTerminal()
        {
            NonTerminal a = new NonTerminal("a");
            Terminal b = new Terminal("b");

            var c = a + b;

            Assert.IsNotNull(c);
            Assert.AreEqual(2, c.Count);
            Assert.IsInstanceOfType(c, typeof(ConcatenationRule));
        }

        [TestMethod]
        public void ConcatenationOfTerminalAndNonTerminal()
        {
            Terminal a = new Terminal("a");
            NonTerminal b = new NonTerminal("b");

            var c = a + b;

            Assert.IsNotNull(c);
            Assert.AreEqual(2, c.Count);
            Assert.IsInstanceOfType(c, typeof(ConcatenationRule));
        }

        [TestMethod]
        public void ConcatenationOfNonTerminalAndString()
        {
            NonTerminal a = new NonTerminal("a");

            var c = a + "b";

            Assert.IsNotNull(c);
            Assert.AreEqual(2, c.Count);
            Assert.IsInstanceOfType(c, typeof(ConcatenationRule));
        }

        [TestMethod]
        public void ConcatenationOfStringAndNonTerminal()
        {
            NonTerminal b = new NonTerminal("b");

            var c = "a" + b;

            Assert.IsNotNull(c);
            Assert.AreEqual(2, c.Count);
            Assert.IsInstanceOfType(c, typeof(ConcatenationRule));
        }

        [TestMethod]
        public void ConcatenationOfTerminalAndString()
        {
            Terminal a = new Terminal("a");

            var c = a + "b";

            Assert.IsNotNull(c);
            Assert.AreEqual(2, c.Count);
            Assert.IsInstanceOfType(c, typeof(ConcatenationRule));
        }

        [TestMethod]
        public void ConcatenationOfStringAndTerminal()
        {
            Terminal b = new Terminal("b");

            var c = "a" + b;

            Assert.IsNotNull(c);
            Assert.AreEqual(2, c.Count);
            Assert.IsInstanceOfType(c, typeof(ConcatenationRule));
        }

        [TestMethod]
        public void ConcatenateConcatenationRuleWithString()
        {
            ConcatenationRule a = "a";

            var c = a + "b";

            Assert.IsNotNull(c);
            Assert.AreEqual(2, c.Count);
            Assert.IsInstanceOfType(c, typeof(ConcatenationRule));
        }

        [TestMethod]
        public void ConcatenateConcatenationRuleWithTerminal()
        {
            ConcatenationRule a = "a";

            var c = a + new Terminal("b");

            Assert.IsNotNull(c);
            Assert.AreEqual(2, c.Count);
            Assert.IsInstanceOfType(c, typeof(ConcatenationRule));
        }

        [TestMethod]
        public void ConcatenateConcatenationRuleWithNonTerminal()
        {
            ConcatenationRule a = "a";

            var c = a + new NonTerminal("b");

            Assert.IsNotNull(c);
            Assert.AreEqual(2, c.Count);
            Assert.IsInstanceOfType(c, typeof(ConcatenationRule));
        }
    }
}

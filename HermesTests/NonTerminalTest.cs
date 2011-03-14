using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hermes;
using Hermes.Grammar;

namespace HermesTests
{
    [TestClass]
    public class NonTerminalTest
    {
        [TestMethod]
        public void ConstructANonTerminal()
        {
            string name = "foo";
            NonTerminal n = new NonTerminal(name);
            Assert.AreEqual(name, n.Name);
            Assert.AreEqual(name, n.ToString());
        }

        [TestMethod]
        public void NonTerminalPlusNonTerminalReturnsRule()
        {
            NonTerminal a = new NonTerminal("a");
            NonTerminal b = new NonTerminal("b");
            Rule operatorResult = a + b;
            Rule methodResult = NonTerminal.Plus(a, b);

            Assert.IsNotNull(operatorResult);
            Assert.IsNotNull(methodResult);
            Assert.AreEqual(operatorResult, methodResult);
        }

        [TestMethod]
        public void NonTerminalPlusTerminalReturnsRule()
        {
            NonTerminal a = new NonTerminal("a");
            Terminal b = new Terminal("b");
            Rule operatorResult = a + b;
            Rule methodResult = NonTerminal.Plus(a, b);

            Assert.IsNotNull(operatorResult);
            Assert.IsNotNull(methodResult);
            Assert.AreEqual(operatorResult, methodResult);
        }

        [TestMethod]
        public void TerminalPlusNonTerminalReturnsRule()
        {
            Terminal a = new Terminal("a");
            NonTerminal b = new NonTerminal("b");
            Rule operatorResult = a + b;
            Rule methodResult = NonTerminal.Plus(a, b);

            Assert.IsNotNull(operatorResult);
            Assert.IsNotNull(methodResult);
            Assert.AreEqual(operatorResult, methodResult);
        }

        [TestMethod]
        public void NonTerminalPlusStringReturnsRule()
        {
            NonTerminal a = new NonTerminal("a");
            string b = "b";
            Rule operatorResult = a + b;
            Rule methodResult = NonTerminal.Plus(a, b);

            Assert.IsNotNull(operatorResult);
            Assert.IsNotNull(methodResult);
            Assert.AreEqual(operatorResult, methodResult);
        }

        [TestMethod]
        public void StringPlusNonTerminalReturnsRule()
        {
            string a = "a";
            NonTerminal b = new NonTerminal("b");
            Rule operatorResult = a + b;
            Rule methodResult = NonTerminal.Plus(a, b);

            Assert.IsNotNull(operatorResult);
            Assert.IsNotNull(methodResult);
            Assert.AreEqual(operatorResult, methodResult);
        }
    }
}

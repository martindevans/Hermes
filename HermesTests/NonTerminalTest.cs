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
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hermes.Grammar;

namespace HermesTests
{
    [TestClass]
    public class ImplicitCastingTests
    {
        [TestMethod]
        public void ConcatenationOfTypedTerminals()
        {
            Terminal a = new Terminal("a");
            Terminal b = new Terminal("b");

            var c = a + b;

            Assert.IsInstanceOfType(c, typeof(ConcatenationRule));
            Assert.IsNotNull(c);
        }

        public void ConcatenationOfTypedNonTerminals()
        {
            NonTerminal a = new NonTerminal("a");
            NonTerminal b = new NonTerminal("b");

            var c = a + b;

            Assert.IsInstanceOfType(c, typeof(ConcatenationRule));
            Assert.IsNotNull(c);
        }

        public void ConcatenationOfUntypedTerminals()
        {
            string a = "a";
            string b = "b";

            ConcatenationRule c = a + b;

            Assert.IsNotNull(c);
        }
    }
}

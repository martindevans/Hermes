using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hermes.Grammar;
using Hermes;

namespace HermesTests
{
    [TestClass]
    public class TerminalTest
    {
        [TestMethod]
        public void ConstructTerminal()
        {
            Terminal t = new Terminal("Test Terminal", ".*");
        }

        [TestMethod]
        public void CastStringIntoTerminal()
        {
            string s = "aaa";
            Terminal t = s;

            Assert.IsNotNull(t);
        }

        [TestMethod]
        public void MatchAString()
        {
            Terminal t = new Terminal("AAA", "AAA");

            string aString = "AAABBB";

            string match;
            Assert.IsTrue(t.Match(aString, 0, out match));
            Assert.AreEqual("AAA", match);
        }

        [TestMethod]
        public void DoNotMatchAString()
        {
            Terminal t = new Terminal("AAA", "AAA");

            string aString = "BBB";

            string match;
            Assert.IsFalse(t.Match(aString, 0, out match));
            Assert.IsNull(match);
        }

        [TestMethod]
        public void DoNotMatchAStringNotAtIndexZero()
        {
            Terminal t = new Terminal("AAA", "AAA");

            string aString = "BBBAAA";

            string match;
            Assert.IsFalse(t.Match(aString, 0, out match));
            Assert.IsNull(match);
        }

        [TestMethod]
        public void MatchAStringNotAtIndexZeroButWithOffset()
        {
            Terminal t = new Terminal("AAA", "AAA");

            string aString = "BBBAAA";

            string match;
            Assert.IsTrue(t.Match(aString, 3, out match));
            Assert.AreEqual("AAA", match);
        }

        [TestMethod]
        public void TerminalPlusTerminalReturnsRule()
        {
            Terminal a = new Terminal("a");
            Terminal b = new Terminal("b");
            ConcatenationRule operatorResult = a + b;
            ConcatenationRule methodResult = Terminal.Plus(a, b);

            Assert.IsNotNull(operatorResult);
            Assert.IsNotNull(methodResult);
            Assert.AreEqual(operatorResult, methodResult);
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hermes.Grammar;

namespace HermesTests
{
    [TestClass]
    public class BnfTermTest
    {
        [TestMethod]
        public void CastStringIntoTerminal()
        {
            string s = "aaa";
            Terminal t = s;

            Assert.IsNotNull(t);
        }

        [TestMethod]
        public void ConcatenateTerms()
        {
            BnfTerm a = new Terminal("a");
            BnfTerm b = new Terminal("b");

            ConcatenationRule c = a + b;

            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void AlternateTerms()
        {
            BnfTerm a = new Terminal("a");
            BnfTerm b = new Terminal("b");

            RuleAlternation c = a | b;

            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void CastBnfTermIntoConcatenationRule()
        {
            BnfTerm a = new Terminal("a");

            ConcatenationRule c = a;

            Assert.IsNotNull(c);
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hermes.Grammar;

namespace HermesTests
{
    [TestClass]
    public class ConcatenationRuleTest
    {
        [TestMethod]
        public void ConcatenateConcatenationRules()
        {
            ConcatenationRule a = new ConcatenationRule(new Terminal("aaa"));
            ConcatenationRule b = new ConcatenationRule(new Terminal("aaa"));

            ConcatenationRule c = a + b;

            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void CastConcatenationRuleIntoAlternationRule()
        {
            RuleAlternation a = new ConcatenationRule(new Terminal("aaa"));

            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void CastStringIntoConcatenationRule()
        {
            ConcatenationRule a = "aaa";

            Assert.IsNotNull(a);
        }

        public void CastTerminalIntoConcatenationRule()
        {
            ConcatenationRule a = new Terminal("aaa");

            Assert.IsNotNull(a);
        }
    }
}

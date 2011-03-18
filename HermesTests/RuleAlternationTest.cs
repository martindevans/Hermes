using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hermes.Bnf;

namespace HermesTests
{
    [TestClass]
    public class RuleAlternationTest
    {
        [TestMethod]
        public void ImplicitCastFromStringToRuleAlternation()
        {
            RuleAlternation foo = "...";
            Assert.AreEqual(@"\.\.\.", (foo.First().First() as Terminal).Regex.ToString());
        }
    }
}

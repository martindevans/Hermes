using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hermes.Bnf;
using Hermes;
using Hermes.Parsers;
using HermesTests.Grammars;

namespace HermesTests
{
    [TestClass]
    public class RecursiveDescentTests
    {
        [TestMethod]
        public void ConstructRecursiveDescentParser()
        {
            Grammar g = new MatchedBrackets("(", ")");

            Parser parser = new RecursiveDescentParser(g);

            var p = parser.Parse("(())");

            Assert.IsNotNull(p);
            Assert.IsFalse(p.Root.IsLeaf);
            Assert.AreEqual("S", p.Root.NonTerminal.Name);
            Assert.AreEqual(3, p.Root.Children.Count);
        }
    }
}

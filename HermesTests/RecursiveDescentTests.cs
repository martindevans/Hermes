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
        public void ParseMatchedBracketsWithRecursiveDescent()
        {
            Grammar g = new MatchedBrackets("(", ")");

            Parser parser = new RecursiveDescentParser(g);

            var p = parser.Parse("(())");

            Assert.IsNotNull(p);
            Assert.IsFalse(p.Root.IsLeaf);
            Assert.AreEqual("S", p.Root.NonTerminal.Name);
            Assert.AreEqual(3, p.Root.Children.Count);

            Assert.IsTrue(p.Root.Children[0].IsLeaf);
            Assert.AreEqual("(", p.Root.Children[0].Token.Value);
            Assert.IsFalse(p.Root.Children[1].IsLeaf);
            Assert.IsTrue(p.Root.Children[2].IsLeaf);
            Assert.AreEqual(")", p.Root.Children[2].Token.Value);

            Assert.IsTrue(p.Root.Children[1].Children[0].IsLeaf);
            Assert.AreEqual("(", p.Root.Children[1].Children[0].Token.Value);
            Assert.IsFalse(p.Root.Children[1].Children[1].IsLeaf);
            Assert.IsTrue(p.Root.Children[1].Children[2].IsLeaf);
            Assert.AreEqual(")", p.Root.Children[1].Children[2].Token.Value);

            Assert.AreEqual(1, p.Root.Children[1].Children[1].Children.Count);
            Assert.IsTrue(p.Root.Children[1].Children[1].Children[0].IsLeaf);
            Assert.AreEqual("", p.Root.Children[1].Children[1].Children[0].Token.Value);
        }
    }
}

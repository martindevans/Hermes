using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hermes.Bnf;
using Hermes.Parsers;

namespace HermesTests
{
    [TestClass]
    public class SLR1Test
    {
        [TestMethod]
        public void ParseValidString()
        {
            NonTerminal S, T, F;
            LRParserBase parser = new SLR1(LR0Test.ConstructTestGrammar(out S, out T, out F));

            ParseTree tree = parser.Parse("((x) * t) * foo");

            Assert.IsNotNull(tree);

            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(3, tree.Root.Children.Count);

            Assert.IsNotNull(tree.Root.Children[0].NonTerminal);
            Assert.AreEqual("*", tree.Root.Children[1].Token.Value);
            Assert.IsNotNull(tree.Root.Children[2].NonTerminal);

            Assert.AreEqual("foo", tree.Root.Children[2].Children[0].Token.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ParseException))]
        public void ParseInvalidEarlyTerminatingString()
        {
            NonTerminal S, T, F;
            LR0 parser = new SLR1(LR0Test.ConstructTestGrammar(out S, out T, out F));

            var tree = parser.Parse("((x)*t)*");
        }

        [TestMethod]
        [ExpectedException(typeof(ParseException))]
        public void ParseInvalidUnexpectedCharacterString()
        {
            NonTerminal S, T, F;
            LR0 parser = new SLR1(LR0Test.ConstructTestGrammar(out S, out T, out F));

            var tree = parser.Parse("((x)*t)**");
        }
    }
}

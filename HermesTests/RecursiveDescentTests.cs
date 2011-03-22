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

            parser.Parse("((()(()))())");
        }
    }
}

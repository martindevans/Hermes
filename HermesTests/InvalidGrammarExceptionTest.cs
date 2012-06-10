using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hermes.Bnf;

namespace HermesTests
{
    [TestClass]
    public class InvalidGrammarExceptionTest
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidGrammarException))]
        public void Throws()
        {
            throw new InvalidGrammarException("Test");
        }
    }
}

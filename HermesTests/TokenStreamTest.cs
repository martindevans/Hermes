using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Hermes;
using Hermes.Grammar;
using Hermes.Tokenising;

namespace HermesTests
{
    [TestClass]
    public class TokenStreamTest
    {
        [TestMethod]
        public void ConstructATokenStream()
        {
            MemoryStream m = new MemoryStream();
            new StreamWriter(m).WriteLine("Hello World!");
            m.Position = 0;

            TokenStream t = new TokenStream(m, new HashSet<Terminal>());
        }

        [TestMethod]
        public void ReadATokenStream()
        {
            var myString = "Hello World!";

            MemoryStream m = new MemoryStream();
            var w = new StreamWriter(m);
            w.Write(myString);
            w.Flush();

            m.Position = 0;

            Terminal singleCharacter = new Terminal("Match a single character", ".");

            TokenStream t = new TokenStream(m, singleCharacter);
            Token[] characters = t.ToArray();

            Assert.AreEqual(myString.Length, characters.Length);

            for (int i = 0; i < myString.Length; i++)
                Assert.AreEqual(myString[i].ToString(), characters[i].Value);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void InvalidInputStream()
        {
            var myString = "Hello World!";

            MemoryStream m = new MemoryStream();
            var w = new StreamWriter(m);
            w.Write(myString);
            w.Flush();

            m.Position = 0;

            Terminal singleCharacter = new Terminal("Match a single A", "A");

            TokenStream t = new TokenStream(m, singleCharacter);
            Token[] characters = t.ToArray();
        }
    }
}

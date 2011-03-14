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

        [TestMethod]
        public void CorrectLineAndColumnNumbers()
        {
            var input = "Hello World!\nSecond line\r\nFoo";

            MemoryStream m = new MemoryStream();
            var w = new StreamWriter(m);
            w.Write(input);
            w.Flush();
            m.Position = 0;

            var word = new Terminal("Word", "\\S+");
            var whitespace = new Terminal("Whitespace", " |\n|\r");

            var lexer = new TokenStream(m, word, whitespace);
            Token[] tokens = lexer.ToArray();

            Assert.AreEqual(10, tokens.Length);
            AssertToken(tokens[0], "Word", 1, 1);
            AssertToken(tokens[1], "Whitespace", 1, 6);
            AssertToken(tokens[2], "Word", 1, 7);
            AssertToken(tokens[3], "Whitespace", 1, 13);
            AssertToken(tokens[4], "Word", 2, 1);
            AssertToken(tokens[5], "Whitespace", 2, 7);
            AssertToken(tokens[6], "Word", 2, 8);
            AssertToken(tokens[7], "Whitespace", 2, 12);
            AssertToken(tokens[8], "Whitespace", 2, 13);
            AssertToken(tokens[9], "Word", 3, 1);
        }

        private void AssertToken(Token token, string name, int line, int column)
        {
            Assert.AreEqual(name, token.Terminal.Name);
            Assert.AreEqual(line, token.Line);
            Assert.AreEqual(column, token.Column);
        }
    }
}

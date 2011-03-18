using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hermes.Bnf;
using Hermes;

namespace HermesTests
{
    [TestClass]
    public class GrammarTest
    {
        [TestMethod]
        public void ConstructAGrammar()
        {
            NonTerminal op = new NonTerminal("Operator");
            NonTerminal expr = new NonTerminal("Expression");

            Terminal number = new Terminal("Number", @"\b\d+\b");
            Terminal plus = new Terminal("Plus", @"\+");
            Terminal multiply = @"\*";

            op.Rules = plus | multiply;
            expr.Rules = number + op + expr;

            Terminal whitespace = new Terminal("Whitespace", " |\n|\r", true);
            Grammar g = new Grammar(expr, whitespace);

            Assert.AreEqual(3, g.Terminals.Count());
            Assert.AreEqual(2, g.NonTerminals.Count());

            Lexer lexer = g.CreateLexer("1 + 2 * 3");

            Assert.AreEqual(5, lexer.Count());
        }
    }
}

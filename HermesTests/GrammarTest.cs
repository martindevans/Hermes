using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hermes.Grammar;
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
            Terminal whitespace = new Terminal("Whitespace", " |\n|\r", true);

            op.Rules = plus | multiply;
            expr.Rules = number + op + expr;

            throw new NotImplementedException();
            //Grammar g = new Grammar(expr);
        }
    }
}

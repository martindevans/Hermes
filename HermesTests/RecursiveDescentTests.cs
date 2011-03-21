using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hermes.Bnf;
using Hermes;
using Hermes.Parsers;

namespace HermesTests
{
    [TestClass]
    public class RecursiveDescentTests
    {
        [TestMethod]
        public void ConstructRecursiveDescentParser()
        {
            NonTerminal op = new NonTerminal("Operator");
            NonTerminal expr = new NonTerminal("Expression");

            Terminal number = new Terminal("Number", @"\b\d+\b");
            Terminal plus = new Terminal("Plus", @"\+");
            Terminal multiply = @"\*";

            op.Rules = plus | multiply;
            expr.Rules = number + op + expr;

            Grammar g = new Grammar(expr);

            IParser parser = new RecursiveDescentParser(g);
        }


    }
}

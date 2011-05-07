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
    public class LR0Test
    {
        [TestMethod]
        public void ConstructAutomaton()
        {
            NonTerminal S = new NonTerminal("S");
            NonTerminal T = new NonTerminal("T");
            NonTerminal F = new NonTerminal("F");

            S.Rules = T + "$";
            T.Rules = T + "*" + F | F;
            F.Rules = "(" + T + ")" | new Terminal("ID", "([A-Z]|[a-z])+");

            Grammar g = new Grammar(S, new Terminal(" "));

            Automaton a = LR0.CreateAutomaton(g);

            Assert.AreEqual(5, a.Terminals.Count());
            Assert.AreEqual(9 + 1, a.States.Count()); //example shows 9 states, we have 10 due to the finishing state also being considered a state
        }
    }
}

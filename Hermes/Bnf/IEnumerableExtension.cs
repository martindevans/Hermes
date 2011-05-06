using System;
using System.Collections.Generic;
using System.Linq;

namespace Hermes.Bnf
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<Item> Closure(this IEnumerable<Item> items, Grammar grammar)
        {
            HashSet<Item> closure = new HashSet<Item>(items);
            HashSet<Item> toAdd = new HashSet<Item>();

            do
            {
                toAdd.Clear();
                foreach (var item in closure)
                {
                    BnfTerm term = item.Production.Body[item.Position];

                    if (term is NonTerminal)
                    {
                        NonTerminal nonTerm = term as NonTerminal;
                        foreach (var production in grammar.Productions.Where(a => a.Head.Equals(nonTerm)))
                            toAdd.Add(new Item(production, 0));
                    }
                }
            }
            while (closure.UnionWithAddedCount(toAdd) > 0);

            return closure;
        }

        public static IEnumerable<Item> Goto(this IEnumerable<Item> state, Terminal symbol, Grammar grammar)
        {
            HashSet<Item> items = new HashSet<Item>();

            foreach (var item in state)
            {
                BnfTerm term = item.Production.Body[item.Position];

                if (term is Terminal)
                {
                    if ((term as Terminal).Equals(symbol))
                        items.Add(new Item(item.Production, item.Position + 1));
                }
            }

            return items.Closure(grammar);
        }
    }
}

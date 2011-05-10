using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.Parsers;

namespace Hermes.Bnf
{
    public static class IEnumerableExtension
    {
        public static ParseState Closure(this IEnumerable<Item> items, Grammar grammar)
        {
            HashSet<Item> closure = new HashSet<Item>(items);
            HashSet<Item> toAdd = new HashSet<Item>();

            do
            {
                toAdd.Clear();
                foreach (var item in closure)
                {
                    if (item.Position == item.Production.Body.Length)
                        continue;

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

            return new ParseState(closure, IsAcceptingState(closure, grammar));
        }

        public static ParseState Goto(this IEnumerable<Item> state, BnfTerm symbol, Grammar grammar)
        {
            HashSet<Item> items = new HashSet<Item>();

            foreach (var item in state)
            {
                if (item.Production.Body.Length == item.Position)
                    continue;

                BnfTerm term = item.Production.Body[item.Position];

                if (term.Equals(symbol))
                    items.Add(new Item(item.Production, item.Position + 1));
            }

            return items.Closure(grammar);
        }

        public static bool IsAcceptingState(this IEnumerable<Item> items, Grammar grammar)
        {
            return items
                .Where(a => a.Production.Head.Equals(grammar.Root))
                .Any(a => a.Position == a.Production.Body.Length);
        }
    }
}

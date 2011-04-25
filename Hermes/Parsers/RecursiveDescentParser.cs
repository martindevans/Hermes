using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermes.Bnf;
using System.IO;
using System.Collections.Concurrent;
using Hermes.Tokenising;

namespace Hermes.Parsers
{
    public class RecursiveDescentParser
        : Parser
    {
        private Grammar g;

        private Dictionary<NonTerminal, Dictionary<Terminal, Production?>> predictiveParseTable = new Dictionary<NonTerminal, Dictionary<Terminal, Production?>>();

        public RecursiveDescentParser(Grammar g)
        {
            this.g = g;

            ConstructPredictiveParseTable();
        }

        private void ConstructPredictiveParseTable()
        {
            foreach (var nonTerminals in g.NonTerminals)
	        {
                var dict = new Dictionary<Terminal, Production?>();
                predictiveParseTable[nonTerminals] = dict;

                foreach (var terminal in g.Terminals)
                    dict[terminal] = null;
	        }

            foreach (var production in g.Productions)
            {
                var first = g.GetFirstSet(production.Body);
                foreach (var element in first)
                {
                    if (production.Body.All(a => a.IsNullable))
                        foreach (var a in g.GetFollowSet(production.Head))
                            Assign(production.Head, a, production);
                    else
                        Assign(production.Head, element, production);
                }
            }
        }

        private void Assign(NonTerminal head, Terminal term, Production production)
        {
            if (predictiveParseTable[head][term].HasValue)
                throw new InvalidGrammarException("Grammar is not LL(1)");

            predictiveParseTable[head][term] = production;
        }

        public override ParseTree Parse(Stream input)
        {
            var root = Parse(new StreamReader(input), g.Root);

            return new ParseTree(root);
        }

        private ParseTreeNode Parse(StreamReader input, NonTerminal root)
        {
            IEnumerator<Token> lexer = g.CreateLexer(input.ReadToEnd()).GetEnumerator();

            var eof = !lexer.MoveNext();

            if (eof)
            {
                if (root.Rules.Any(a => a.IsNullable()))
                    return new ParseTreeNode(null, new Token(new Terminal(""), "", 0, 0));
                else
                    throw new ParseException("Empty string not matched by grammar");
            }

            return Parse(lexer, root, null);
        }

        private ParseTreeNode Parse(IEnumerator<Token> lexer, NonTerminal current, ParseTreeNode parent)
        {
            Production? prod = predictiveParseTable[current][lexer.Current.Terminal];

            if (!prod.HasValue)
            {
                if (current.IsNullable)
                {
                    var n = new ParseTreeNode(parent, current);
                    var c = new ParseTreeNode(n, new Token(new Terminal(""), "", lexer.Current.Line, lexer.Current.Column));

                    return n;
                }
                else
                    throw new ParseException("Unable to match " + lexer.Current + " at line " + lexer.Current.Line + " column " + lexer.Current.Column);
            }

            ParseTreeNode node = new ParseTreeNode(parent, current);
            List<ParseTreeNode> nodes = new List<ParseTreeNode>();

            for (int i = 0; i < prod.Value.Body.Length; i++)
            {
                var term = prod.Value.Body[i];
                if (term is Terminal)
                {
                    string match;
                    (term as Terminal).Match(lexer.Current.Value, 0, out match);

                    nodes.Add(new ParseTreeNode(node, lexer.Current));

                    if (!lexer.MoveNext())
                        for (int j = i + 1; j < prod.Value.Body.Length; j++)
                            if (!prod.Value.Body[j].IsNullable)
                                throw new ParseException("unexpected EOF");
                }
                else if (term is NonTerminal)
                {
                    nodes.Add(Parse(lexer, term as NonTerminal, node));
                }
                else
                    throw new Exception("wtf?");
            }

            return node;
        }
    }
}

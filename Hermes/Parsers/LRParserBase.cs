using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermes.Bnf;
using System.IO;
using Hermes.Tokenising;

namespace Hermes.Parsers
{
    public abstract class LRParserBase
        :Parser
    {
        public LRParserBase(Grammar g)
            :base(g)
        {
        }

        protected abstract ParserAction Action(ParseState state, Token token, out Production production, out ParseState nextState);

        protected abstract ParseState Goto(ParseState state, NonTerminal nonTerminal);

        public enum ParserAction
        {
            Shift,
            Reduce,
            Accept,
            Error,
            Start
        }

        private ParseState MakeStateState(NonTerminal root, Grammar g)
        {
            return new HashSet<Item>(root.Rules.Select(a => new Item(new Production(root, a), 0))).Closure(g);
        }

        protected override ParseTreeNode Parse(Lexer lexer, NonTerminal root)
        {
            IEnumerator<Token> tokens = lexer.GetEnumerator();

            var eof = !tokens.MoveNext();

            if (eof)
            {
                if (root.Rules.Any(r => r.IsNullable()))
                    return new ParseTreeNode(null, new Token(new Terminal(""), "", 0, 0));
                else
                    throw new ParseException("Empty string not matched by grammar");
            }

            Stack<ParseState> states = new Stack<ParseState>();
            states.Push(MakeStateState(root, Grammar));
            ParserAction action = ParserAction.Start;

            do
            {
                Production reduceProduction;
                ParseState shiftState;
                action = Action(states.Peek(), eof ? null : tokens.Current, out reduceProduction, out shiftState);

                switch (action)
                {
                    case ParserAction.Accept:
                        break;
                    case ParserAction.Error:
                        throw new ParseException(tokens.Current.ToString());
                    case ParserAction.Start:
                    default:
                        throw new NotImplementedException();

                    case ParserAction.Shift:
                        states.Push(shiftState);
                        eof = !tokens.MoveNext();
                        break;

                    case ParserAction.Reduce:
                        for (int i = 0; i < reduceProduction.Body.Length; i++)
                            states.Pop();
                        states.Push(Goto(states.Peek(), reduceProduction.Head));
                        break;
                }

            } while (action != ParserAction.Accept);

            return null;
        }
    }
}

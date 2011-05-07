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

        protected abstract KeyValuePair<ParserAction, int> Action(int state, Token token);

        protected abstract int Goto(int state, NonTerminal nonTerminal);

        protected abstract Production GetProduction(int index);

        public enum ParserAction
        {
            Shift,
            Reduce,
            Accept,
            Error,
            Start
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

            Token a = tokens.Current;
            Stack<int> states = new Stack<int>();
            KeyValuePair<ParserAction, int> action = new KeyValuePair<ParserAction, int>(ParserAction.Start, 0);

            do
            {
                action = Action(states.Peek(), a);

                switch (action.Key)
                {
                    case ParserAction.Accept:
                        break;
                    case ParserAction.Error:
                        throw new ParseException(a.ToString());
                    case ParserAction.Start:
                    default:
                        throw new NotImplementedException();

                    case ParserAction.Shift:
                        states.Push(action.Value);
                        break;

                    case ParserAction.Reduce:
                        var production = GetProduction(action.Value);
                        for (int i = 0; i < production.Body.Length; i++)
                            states.Pop();
                        states.Push(Goto(states.Peek(), production.Head));
                        break;
                }

            } while (action.Key != ParserAction.Accept);

            throw new NotImplementedException();
        }
    }
}

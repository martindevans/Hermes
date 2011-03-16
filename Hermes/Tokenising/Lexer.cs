using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Hermes.Grammar;
using Hermes.Tokenising;
using System.Text.RegularExpressions;

namespace Hermes
{
    /// <summary>
    /// Returns a stream of tokens from an input text stream.
    /// </summary>
    public class Lexer
        : IEnumerable<Token>
    {
        #region fields
        private static readonly Regex newLineRegex = new Regex(@"\n|\r\n|\f");
        private string input;
        private Terminal[] terminals;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Lexer"/> class.
        /// </summary>
        /// <param name="stream">Stream of input characters.</param>
        /// <param name="terminals">The set of terminals to match.</param>
        public Lexer(Stream stream, params Terminal[] terminals)
            : this(stream, terminals as IEnumerable<Terminal>)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Lexer"/> class.
        /// </summary>
        /// <param name="stream">Stream of input characters.</param>
        /// <param name="terminals">The set of terminals to match.</param>
        public Lexer(Stream stream, IEnumerable<Terminal> terminals)
        {
            using (var r = new StreamReader(new BufferedStream(stream)))
                this.input = r.ReadToEnd();

            this.terminals = terminals.ToArray();
        }
        #endregion

        #region IEnumerable
        /// <summary>
        /// Returns matching tokens from the input stream. In ambiguous situations matches the longest token available.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Thrown if the next token in the input cannot be matched to any of the given terminals.</exception>
        public IEnumerator<Token> GetEnumerator()
        {
            int index = 0;
            int line = 1;
            int column = 1;

            while (index < input.Length)
            {
                Token bestToken = null;
                foreach (var terminal in terminals)
                {
                    string match = null;
                    if (terminal.Match(input, index, out match))
                    {
                        if (bestToken == null || match.Length > bestToken.Value.Length)
                            bestToken = new Token(terminal, match, line, column);
                    }
                }

                if (bestToken == null)
                    throw new InvalidDataException(string.Format("Unrecognised symbol '{0}' at line {1}, column {2}.", input[index], line, column));

                CalculatePosition(index, bestToken.Value.Length, ref line, ref column);
                index += bestToken.Value.Length;

                if (!bestToken.Terminal.IsIgnored)
                    yield return bestToken;
            }
        }

        private void CalculatePosition(int index, int length, ref int line, ref int column)
        {
            var end = index + length;

            column += length;

            while (index < end)
            {
                var match = newLineRegex.Match(input, index, end - index);
                
                if (!match.Success)
                    return;

                line++;
                index = match.Index + match.Length;
                column = end - index + 1;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}

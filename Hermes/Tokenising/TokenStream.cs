using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Hermes.Grammar;
using Hermes.Tokenising;

namespace Hermes
{
    /// <summary>
    /// Returns a stream of tokens from an input text stream.
    /// </summary>
    public class TokenStream
        : IEnumerable<Token>
    {
        #region fields
        string input;
        Terminal[] terminals;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenStream"/> class.
        /// </summary>
        /// <param name="stream">Stream of input characters.</param>
        /// <param name="terminals">The set of terminals to match.</param>
        public TokenStream(Stream stream, params Terminal[] terminals)
            : this(stream, terminals as IEnumerable<Terminal>)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenStream"/> class.
        /// </summary>
        /// <param name="stream">Stream of input characters.</param>
        /// <param name="terminals">The set of terminals to match.</param>
        public TokenStream(Stream stream, IEnumerable<Terminal> terminals)
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
        /// <exception cref="Exception">Thrown if the next token in the input cannot be matched to any of the given terminals.</exception>
        public IEnumerator<Token> GetEnumerator()
        {
            int index = 0;
            while (index < input.Length)
            {
                Token bestToken = null;
                foreach (var terminal in terminals)
                {
                    string match = null;
                    if (terminal.Match(input, index, out match))
                    {
                        if (bestToken == null || match.Length > bestToken.Value.Length)
                            bestToken = new Token(terminal, match);
                    }
                }

                if (bestToken == null)
                    throw new Exception(string.Format("Unrecognised symbol '{0}' at index {1}", input[index], index));

                index += bestToken.Value.Length;

                yield return bestToken;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}

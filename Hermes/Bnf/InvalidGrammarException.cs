using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hermes.Bnf
{
    public class InvalidGrammarException
        :Exception
    {
        public InvalidGrammarException(String message)
            :base(message)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hermes.Parsers
{
    public class ParseException
        :Exception
    {
        public ParseException(String message)
            :base(message)
        {

        }
    }
}

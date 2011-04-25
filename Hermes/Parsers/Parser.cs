using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hermes.Parsers
{
    public abstract class Parser
    {
        public abstract ParseTree Parse(Stream input);

        public ParseTree Parse(String input)
        {
            MemoryStream m = new MemoryStream();
            StreamWriter w = new StreamWriter(m);
            w.Write(input);
            w.Flush();
            m.Position = 0;

            return Parse(m);
        }
    }
}

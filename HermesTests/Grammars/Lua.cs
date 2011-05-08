using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermes.Bnf;

namespace HermesTests.Grammars
{
    public class Lua
        :Grammar
    {
        public Lua()
            : base(ConstructGrammar(), GetWhitespace())
        {
        }

        private static Terminal[] GetWhitespace()
        {
            return new Terminal[] { new Terminal("Whitespace", " |\n|\r|\t", true) };
        }

        private static NonTerminal ConstructGrammar()
        {
            NonTerminal chunk = new NonTerminal("chunk");
            NonTerminal block = new NonTerminal("block");
            NonTerminal stat = new NonTerminal("stat");
            NonTerminal laststat = new NonTerminal("laststat");
            NonTerminal funcname = new NonTerminal("funcname");
            NonTerminal varlist = new NonTerminal("varlist");
            NonTerminal var = new NonTerminal("var");
            NonTerminal namelist = new NonTerminal("namelist");
            NonTerminal explist = new NonTerminal("explist");
            NonTerminal exp = new NonTerminal("exp");
            NonTerminal prefixexp = new NonTerminal("prefixexp");
            NonTerminal functioncall = new NonTerminal("functioncall");
            NonTerminal args = new NonTerminal("args");
            NonTerminal function = new NonTerminal("function");
            NonTerminal funcbody = new NonTerminal("funcbody");
            NonTerminal parlist = new NonTerminal("parlist");
            NonTerminal tableConstructor = new NonTerminal("tableConstructor");
            NonTerminal fieldList = new NonTerminal("fieldList");
            NonTerminal field = new NonTerminal("field");
            NonTerminal fieldsep = new NonTerminal("fieldsep");
            NonTerminal binop = new NonTerminal("binop");
            NonTerminal unop = new NonTerminal("unop");

            chunk.Rules = stat.Star() + laststat.Optional() + ";".Optional();

            //block.Rules = chunk;
            //stat.Rules = varlist + "=" + explist
            //    | functioncall
            //    | "do" + block + "end"
            //    | "while" + exp + "do" + block + "end"
            //    | "repeat" + block + "until" + exp
            //    | "if" + exp + "then" + block + ("elseif" + exp + "then" + block).Star() + ("else" + block).Optional() + "end";

            return chunk;
        }
    }
}

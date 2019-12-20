using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Interpreter
{
    public class Amount : Expression
    {
        private int number;

        public Amount(int number)
        {
            this.number = number;
        }

        public string Interpret(InterpreterContext ctx)
        {
            ctx.amount = number;
            return ctx.Execute();
        }
    }
}

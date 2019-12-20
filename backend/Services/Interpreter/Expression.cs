using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Interpreter
{
    interface Expression
    {
        String Interpret(InterpreterContext ctx);
    }
}

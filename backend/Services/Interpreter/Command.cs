using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Interpreter
{
    public class Command : Expression
    {
        private string command;
        private Receiver receiver;

        public Command(string command, Receiver receiver)
        {
            this.command = command;
            this.receiver = receiver;
        }

        public string Interpret(InterpreterContext ctx)
        {
            ctx.command = this.command;
            return receiver.Interpret(ctx);
        }
    }
}

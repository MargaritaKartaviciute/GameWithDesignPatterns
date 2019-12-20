using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Interpreter
{
    public class Receiver : Expression
    {
        private string player;
        private Amount amount;

        public Receiver(String player, Amount amount)
        {
            this.player = player;
            this.amount = amount;
        }

        public string Interpret(InterpreterContext ctx)
        {
            ctx.playerName = this.player;
            if (amount == null)
                return ctx.Execute();
            return amount.Interpret(ctx);
        }
    }
}

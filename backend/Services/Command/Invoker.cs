using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Command
{
    public class Invoker
    {
        private ICommand _command;
        public Invoker(ICommand command)
        {
            _command = command;
        }

        public void ExecuteCommand(string username)
        {
            _command.Execute(username);
        }
    }
}

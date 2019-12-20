using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Command
{
    public interface ICommand
    {
        void Execute(string username);
        void Undo(string username);
    }
}

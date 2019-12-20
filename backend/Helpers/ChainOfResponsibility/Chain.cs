using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Helpers.ChainOfResponsibility
{
    public interface Chain
    {
        void setNextChain(Chain nextChain);
        Message addItem(Item itemToAdd);
    }
}

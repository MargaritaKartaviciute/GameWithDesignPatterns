using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Helpers.ChainOfResponsibility
{
    public class ElixirHealth : Chain
    {
        private Chain nextChain;
        public Message addItem(Item itemToAdd)
        {
            if (itemToAdd.ItemType == (int)Enums.ItemTypes.Elixir && itemToAdd.ItemSubType == 1)
            {
                return itemToAdd.service.CreateElixirTypeHealth(itemToAdd);
            }
            return nextChain.addItem(itemToAdd);
        }

        public void setNextChain(Chain nextChain)
        {
            this.nextChain = nextChain;
        }
    }
}

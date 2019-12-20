using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Helpers.ChainOfResponsibility
{
    public class ElixirEnergy : Chain
    {
        public Message addItem(Item itemToAdd)
        {
            if (itemToAdd.ItemType == (int)Enums.ItemTypes.Elixir && itemToAdd.ItemSubType == 2)
            {
                return itemToAdd.service.CreateElixirTypeEnergy(itemToAdd);
            }
            return new Message { IsValid = false, MessageText = "Item not classified correctly" };
        }

        public void setNextChain(Chain nextChain)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Helpers.ChainOfResponsibility
{
    public class RegularItem : Chain
    {
        private Chain nextChain;
        public Message addItem(Item itemToAdd)
        {
            if(itemToAdd.ItemType == (int)Enums.ItemTypes.Sword
                || itemToAdd.ItemType == (int)Enums.ItemTypes.Armor)
            {
                return itemToAdd.service.CreateRegularItem(itemToAdd);
            }
            return nextChain.addItem(itemToAdd);
        }

        public void setNextChain(Chain nextChain)
        {
            this.nextChain = nextChain;
        }
    }
}

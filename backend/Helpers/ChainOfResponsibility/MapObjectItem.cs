using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Helpers.ChainOfResponsibility
{
    public class MapObjectItem : Chain
    {
        private Chain nextChain;
        public Message addItem(Item itemToAdd)
        {
            if (itemToAdd.ItemType == (int)Enums.ItemTypes.TreeHelper
                || itemToAdd.ItemType == (int)Enums.ItemTypes.WaterHelper
                || itemToAdd.ItemType == (int)Enums.ItemTypes.RockHelper)
            {
                return itemToAdd.service.CreateMapObjectItemHelper(itemToAdd);
            }
            return nextChain.addItem(itemToAdd);
        }

        public void setNextChain(Chain nextChain)
        {
            this.nextChain = nextChain;
        }
    }
}

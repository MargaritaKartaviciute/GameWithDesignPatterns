using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Helpers.ChainOfResponsibility
{
    public class ChainsCreation
    {
        public static Message AddItem(Item ItemToAdd)
        {
            Chain regular = new RegularItem();
            Chain map = new MapObjectItem();
            Chain ElixirHealth = new ElixirHealth();
            Chain ElixirEnergy = new ElixirEnergy();
            regular.setNextChain(map);
            map.setNextChain(ElixirHealth);
            ElixirHealth.setNextChain(ElixirEnergy);
            return regular.addItem(ItemToAdd);
        }
    }
}

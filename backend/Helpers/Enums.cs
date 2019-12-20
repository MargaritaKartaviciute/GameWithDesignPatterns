using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Helpers
{
    public static class Enums
    {
        public enum MapObjectTypes
        {
            Water=1,
            Tree=2,
            Rock=3
        }

        public enum ItemTypes
        {
            Sword = 1,
            Armor = 2,
            Elixir = 3,
            TreeHelper = 4,
            WaterHelper = 5,
            RockHelper = 6
        }

        public enum EnergyDeprivators
        {
           WaterEnergyDeprivator = 5,
           RockEnergyDeprivator = 10,
           TreeEnergyDeprivator = 3
        }
    }
}

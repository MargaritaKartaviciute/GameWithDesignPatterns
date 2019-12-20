using backend.Data;
using backend.Models;
using backend.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace backend.Helpers
{
    public interface MapObjectCoins
    {
        int CoinsWorthWithItems { get; set; }
        int CoinAmount();
        bool isCustomAmountOfCoins(SalaContext ctx, string Username);
    }

    public class TreeCoinAmount : MapObjectCoins
    {
        public int CoinsWorthWithItems { get; set; }
        public int CoinAmount()
        {
            if (CoinsWorthWithItems == 0) CoinsWorthWithItems = 10;
            return CoinsWorthWithItems;
        }

        public bool isCustomAmountOfCoins(SalaContext ctx, string Username)
        {
            if(ctx.PlayerItems.Any(a=> a.Item.ItemType == (int)Enums.ItemTypes.TreeHelper && a.Player.UserName == Username))
            {
               ctx.PlayerItems.Where(a => a.Item.ItemType == (int)Enums.ItemTypes.TreeHelper && a.Player.UserName == Username)
                                                 .Select(a=>a.Item)
                                                 .ToList()
                                                 .ForEach(a=> CoinsWorthWithItems += a.Power );
                return true;
            }
            return false;
        }
    }

    public class RockCoinAmount : MapObjectCoins
    {
        public int CoinsWorthWithItems { get; set; }

        public int CoinAmount()
        {
            if (CoinsWorthWithItems == 0) CoinsWorthWithItems = 20;
            return CoinsWorthWithItems;
        }

        public bool isCustomAmountOfCoins(SalaContext ctx, string Username)
        {
            if (ctx.PlayerItems.Any(a => a.Item.ItemType == (int)Enums.ItemTypes.RockHelper && a.Player.UserName == Username))
            {
                ctx.PlayerItems.Where(a => a.Item.ItemType == (int)Enums.ItemTypes.RockHelper && a.Player.UserName == Username)
                                                  .Select(a => a.Item)
                                                  .ToList()
                                                  .ForEach(a => CoinsWorthWithItems += a.Power);
                return true;
            }
            return false;
        }
    }

    public class WaterCoinAmount : MapObjectCoins
    {
        public int CoinsWorthWithItems { get; set; }

        public int CoinAmount()
        {
            if (CoinsWorthWithItems == 0) CoinsWorthWithItems = 1;
            return CoinsWorthWithItems;
        }

        public bool isCustomAmountOfCoins(SalaContext ctx, string Username)
        {
            if (ctx.PlayerItems.Any(a => a.Item.ItemType == (int)Enums.ItemTypes.WaterHelper && a.Player.UserName == Username))
            {
                ctx.PlayerItems.Where(a => a.Item.ItemType == (int)Enums.ItemTypes.WaterHelper && a.Player.UserName == Username)
                                                  .Select(a => a.Item)
                                                  .ToList()
                                                  .ForEach(a => CoinsWorthWithItems += a.Power);
                return true;
            }
            return false;
        }
    }

    public class CustomCoinAmount : MapObjectCoins
    {
        public int CoinsWorthWithItems { get; set; }

        public CustomCoinAmount(int amount)
        {
            this.CoinsWorthWithItems = amount;
        }
        public int CoinAmount()
        {
            return this.CoinsWorthWithItems;
        }

        public bool isCustomAmountOfCoins(SalaContext ctx, string Username)
        {
            throw new System.NotImplementedException();
        }
    }
}

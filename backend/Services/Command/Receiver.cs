using backend.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Command
{
    public class Receiver
    {
        private readonly SalaContext _context;
        private static readonly int WaterEnergyDeprivator = 5;
        private static readonly int RockEnergyDeprivator = 10;
        private static readonly int TreeEnergyDeprivator = 3;
        public Receiver(SalaContext context)
        {
            _context = context;
        }
        public void ChangePlayerEnergyWithWater(string username)
        {
           var player =  _context.Player.FirstOrDefault(x => x.UserName == username);
            if (player != null)
            {
                player.Energy -= WaterEnergyDeprivator;
                _context.SaveChanges();
            }
            
        }
        public void UndoPlayerEnergyWithWaterChanges(string username)
        {
            var player = _context.Player.FirstOrDefault(x => x.UserName == username);
            if (player != null)
            {
                player.Energy += WaterEnergyDeprivator;
                _context.SaveChanges();
            }
        }

        public void ChangePlayerEnergyWithRock(string username)
        {
            var player = _context.Player.FirstOrDefault(x => x.UserName == username);
            if(player != null)
            {
                player.Energy -= RockEnergyDeprivator;
                _context.SaveChanges();
            }
        }
        public void UndoPlayerEnergyWithRockChanges(string username)
        {
            var player = _context.Player.FirstOrDefault(x => x.UserName == username);
            if (player != null)
            {
                player.Energy += RockEnergyDeprivator;
                _context.SaveChanges();
            }
        }

        public void ChangePlayerEnergyWithTree(string username)
        {
            var player = _context.Player.FirstOrDefault(x => x.UserName == username);
            if (player != null)
            {
                player.Energy -= TreeEnergyDeprivator;
                _context.SaveChanges();
            }
            
        }
        public void UndoPlayerEnergyWithTreeChanges(string username)
        {
            var player = _context.Player.FirstOrDefault(x => x.UserName == username);
            if (player != null)
            {
                player.Energy += TreeEnergyDeprivator;
                _context.SaveChanges();
            }
        }

        public void RecalculatePlayerMoney(string username)
        {
            var player = _context.Player.FirstOrDefault(x => x.UserName == username);
            if (player != null)
            {
                var moneyList = _context.PlayerCutItem.Where(x => x.PlayerId == player.Id);
                player.Money = moneyList.Sum(x => x.CoinsWorth);
                _context.SaveChanges();
            }
        }

        public void UndoPlayerMoneyRecalculation(string username)
        {
            var player = _context.Player.FirstOrDefault(x => x.UserName == username);
            if (player != null)
            {
                var moneyList = _context.PlayerCutItem.Where(x => x.PlayerId == player.Id);
                player.Money = moneyList.Sum(x => x.CoinsWorth) - moneyList.Select(x => x.CoinsWorth).FirstOrDefault();
                _context.SaveChanges();
            }
        }

    }
}

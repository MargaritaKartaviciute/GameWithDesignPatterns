using backend.Data;
using backend.Helpers;
using backend.Models;
using backend.Models.MapObjects;
using backend.Services.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static backend.Helpers.Enums;

namespace backend.Services.Template
{
    public class ConfrontationWithWater: PlayerMovementTemplate
    {
        public ConfrontationWithWater(SalaContext context): base( context)
        {}
        public override void CutItem(string username)
        {
            var water = new Water();
            if (water.CoinsWorth.isCustomAmountOfCoins(_context, username))
            {
                water.CoinsWorth = new CustomCoinAmount(water.CoinsWorth.CoinsWorthWithItems);
            }
            int coinsWorth = 0;
            if (/*true ||*/DateTime.Now >= DateTime.Parse("2019-12-24") && DateTime.Now < DateTime.Parse("2019-12-27"))
            {
                ChristmasVisitor visitor = new ChristmasVisitor();
                coinsWorth = water.accept(visitor);
            }
            else if (/*true ||*/DateTime.Now >= DateTime.Parse("2019-12-31") && DateTime.Now < DateTime.Parse("2020-01-02"))
            {
                NewYearVisitor visitor = new NewYearVisitor();
                coinsWorth = water.accept(visitor);
            }
            else
            {
                RegularVisitor visitor = new RegularVisitor();
                coinsWorth = water.accept(visitor);
            }
            PlayerCutItems item = new PlayerCutItems
            {
                PlayerId = _context.Player.FirstOrDefault(a => a.UserName == username).Id,
                CoinsWorth = coinsWorth
            };
            _context.PlayerCutItem.Add(item);
            _context.SaveChanges();
        }
        public override void ChangePlayerEnergy(string username)
        {
            var player = _context.Player.FirstOrDefault(x => x.UserName == username);
            if (player != null)
            {
                player.Energy -= (int)Enums.EnergyDeprivators.WaterEnergyDeprivator;
                _context.SaveChanges();
            }

        }

        public override void DeleteMapObject(int id)
        {
            var water = _context.Water.FirstOrDefault(x => x.Id == id);
            _context.Water.Remove(water);
        }
    }
}

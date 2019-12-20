using backend.Data;
using backend.Helpers;
using backend.Models;
using backend.Models.MapObjects;
using backend.Services.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Template
{
    public class ConfrontationWithRock : PlayerMovementTemplate
    {

        public ConfrontationWithRock(SalaContext context) : base(context)
        { }
        public override void CutItem(string username)
        {
            var rock = new Rock();
            if (rock.CoinsWorth.isCustomAmountOfCoins(_context, username))
            {
                rock.CoinsWorth = new CustomCoinAmount(rock.CoinsWorth.CoinsWorthWithItems);
            }
            int coinsWorth = 0;
            if (/*true ||*/ DateTime.Now >= DateTime.Parse("2019-12-24") && DateTime.Now < DateTime.Parse("2019-12-27"))
            {
                ChristmasVisitor visitor = new ChristmasVisitor();
                coinsWorth = rock.accept(visitor);
            }
            else if ( /*true ||*/DateTime.Now >= DateTime.Parse("2019-12-31") && DateTime.Now < DateTime.Parse("2020-01-02"))
            {
                NewYearVisitor visitor = new NewYearVisitor();
                coinsWorth = rock.accept(visitor);
            }
            else
            {
                RegularVisitor visitor = new RegularVisitor();
                coinsWorth = rock.accept(visitor);
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
                player.Energy -= (int)Enums.EnergyDeprivators.RockEnergyDeprivator;
                _context.SaveChanges();
            }

        }

        public override void DeleteMapObject(int id)
        {
            var rock = _context.Rock.FirstOrDefault(x => x.Id == id);
            _context.Rock.Remove(rock);
        }
    }
}

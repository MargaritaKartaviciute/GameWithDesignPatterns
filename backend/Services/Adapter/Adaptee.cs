using backend.Data;
using backend.Helpers;
using backend.Models;
using backend.Models.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Players
{
    public class Adaptee
    {
        private readonly SalaContext _context;
        public Adaptee(SalaContext context)
        {
            _context = context;
        }
        public void CutItemByType(string username, MapObject CutObject, int mapObjId)
        {
            if (CutObject.CoinsWorth.isCustomAmountOfCoins(_context, username))
            {
                CutObject.CoinsWorth = new CustomCoinAmount(CutObject.CoinsWorth.CoinsWorthWithItems);
            }
            PlayerCutItems item = new PlayerCutItems
            {
                PlayerId = _context.Player.FirstOrDefault(a => a.UserName == username).Id,
                CoinsWorth = CutObject.CoinsWorthAmount
            };
            _context.PlayerCutItem.Add(item);
            _context.SaveChanges();
        }
    }
}

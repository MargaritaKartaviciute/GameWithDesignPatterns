using backend.Data;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Template
{
    public abstract class PlayerMovementTemplate
    {
        public readonly SalaContext _context;
        public PlayerMovementTemplate(SalaContext context)
        {
            _context = context;
        }
        public abstract void CutItem(string username);
        public abstract void ChangePlayerEnergy(string username);
        public abstract void DeleteMapObject(int id);

        private  void RecalculatePlayerMoney(string username)
        {
            var player = _context.Player.FirstOrDefault(x => x.UserName == username);
            if (player != null)
            {
                var moneyList = _context.PlayerCutItem.Where(x => x.PlayerId == player.Id);
                player.Money = moneyList.Sum(x => x.CoinsWorth);
                _context.SaveChanges();
            }
        }
        public virtual void MakeMovement(string username, int objectId)
        {
            CutItem(username);
            RecalculatePlayerMoney(username);
            ChangePlayerEnergy(username);
            DeleteMapObject(objectId);

        }
    }
}

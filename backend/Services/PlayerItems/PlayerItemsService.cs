using backend.Data;
using backend.Helpers;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.PlayerItems
{
    public class PlayerItemsService: IPlayerItemsService
    {
        private readonly SalaContext _context;
        public PlayerItemsService(SalaContext context)
        {
            _context = context;
        }
       public Message GetItemsByUser(string username)
        {
            if(!_context.PlayerItems.Where(x => x.Player.UserName == username).Any())
            {
                return new Message { IsNotFound = true };
            }

            return new Message();
        }
        public Message AddPlayerItem(string username, int itemId)
        {
            if(_context.Player.Any(x => x.UserName == username && !x.IsDead && x.MovesCount > 0))
            {
                var user = _context.Player.Include(a=>a.PlayerCuttedItems).FirstOrDefault(x => x.UserName == username);
                var playerItem = _context.Item.FirstOrDefault(x => x.Id == itemId);
                if (playerItem != null)
                {
                    if(user.CoinsAmount > playerItem.Price)
                    {
                        var newUserItem = new PlayerItem
                        {
                            Player = user,
                            Item = playerItem
                        };

                        _context.Add(newUserItem);
                        _context.SaveChanges();
                        return new Message();
                    }
                    return new Message { IsValid = false, MessageText = "You don't have enough coins" };
                }
                return new Message { IsValid = false, MessageText = "Item not found" };
            }
            return new Message { IsValid = false, MessageText = "user not found" };
        }

        public bool DeletePlayerItem(int id)
        {
            var item = _context.PlayerItems
                .SingleOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.PlayerItems.Remove(item);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

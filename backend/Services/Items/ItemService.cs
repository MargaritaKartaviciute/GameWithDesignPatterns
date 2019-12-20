using backend.Data;
using backend.DataContracts;
using backend.Helpers;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Items
{
    public class ItemService : IItemService
    {
        private readonly SalaContext _context;

        public ItemService(SalaContext context)
        {
            _context = context;
        }
        public List<Item> GetAll()
        {
            return _context.Item
                                  .ToList();
        }

        public Message GetById(int id)
        {
            var item = _context.Item
                          .SingleOrDefault(x => x.Id == id);
            if (item == null) return new Message { IsNotFound = true };
            return new Message();
        }

        public bool DeleteById(int id)
        {
            var item = _context.Item
                .SingleOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.Item.Remove(item);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Message Buy(ItemBuyDataContract itemBuyObject)
        {
            Player player = _context.Player.Where(a => a.UserName == itemBuyObject.Username && !a.IsDead)
                                           .Include(a=>a.PlayerItems)
                                           .Include(a=>a.PlayerCuttedItems)
                                           .FirstOrDefault();
            if(player != null)
            {
                Item item = _context.Item.Where(a => a.Id == itemBuyObject.ItemID).FirstOrDefault();
                if(item != null)
                {
                    if(player.CoinsAmount > item.Price)
                    {
                        _context.PlayerItems.Add(new PlayerItem { Item = item, Player = player });
                        _context.SaveChanges();
                    }
                    return new Message { IsValid = false, MessageText = "item is too expensive" };
                }
                return new Message { IsValid = false, MessageText = "Item not found" };
            }
            return new Message { IsValid = false, MessageText = "Player not found" };
        }

        public Message CreateRegularItem(Item item)
        {
            if(!_context.Item.Any(a=>a.Power == item.Power
                                && a.Name == item.Name
                                && a.Price == item.Price
                                && a.ItemPhotoSrc == item.ItemPhotoSrc
                                && (a.ItemType == (int)Enums.ItemTypes.Sword || a.ItemType == (int)Enums.ItemTypes.Armor)))
            {
                _context.Item.Add(item);
                _context.SaveChanges();
                Debug.WriteLine("Regular item created");
                return new Message();
            }
            return new Message { IsValid = false, MessageText = "Item exists" };
        }

        public Message CreateMapObjectItemHelper(Item item)
        {
            if (!_context.Item.Any(a => a.Power == item.Power
                                 && a.Name == item.Name
                                 && a.Price == item.Price
                                 && a.ItemPhotoSrc == item.ItemPhotoSrc
                                 && (a.ItemType == (int)Enums.ItemTypes.TreeHelper
                                    || a.ItemType == (int)Enums.ItemTypes.WaterHelper
                                    || a.ItemType == (int)Enums.ItemTypes.RockHelper)))
            {
                _context.Item.Add(item);
                _context.SaveChanges();
                Debug.WriteLine("Map object helper created");

                return new Message();
            }
            return new Message { IsValid = false, MessageText = "Item exists" };
        }

        public Message CreateElixirTypeHealth(Item item)
        {
            if (!_context.Item.Any(a => a.Power == item.Power
                                 && a.Name == item.Name
                                 && a.Price == item.Price
                                 && a.ItemPhotoSrc == item.ItemPhotoSrc
                                 && a.ItemType == (int)Enums.ItemTypes.Elixir && a.ItemSubType == 1))
            {
                _context.Item.Add(item);
                _context.SaveChanges();
                Debug.WriteLine("Health elixir created");
                return new Message();
            }
            return new Message { IsValid = false, MessageText = "Item exists" };
        }

        public Message CreateElixirTypeEnergy(Item item)
        {
            if (!_context.Item.Any(a => a.Power == item.Power
                                 && a.Name == item.Name
                                 && a.Price == item.Price
                                 && a.ItemPhotoSrc == item.ItemPhotoSrc
                                 && a.ItemType == (int)Enums.ItemTypes.Elixir && a.ItemSubType == 2))
            {
                _context.Item.Add(item);
                _context.SaveChanges();
                Debug.WriteLine("Energy elixir created");
                return new Message();
            }
            return new Message { IsValid = false, MessageText = "Item exists" };
        }
    }
}

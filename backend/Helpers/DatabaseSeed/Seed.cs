using backend.Data;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Helpers.DatabaseSeed
{
    public class Seed
    {
        public static void SeedData(SalaContext _db)
        {
            if (_db != null && !_db.Item.Any())
            {
                _db.Item.AddRange(new Item[] {
                    new Item{Name="Bhohn", Power=50, ItemType=(int)Enums.ItemTypes.Armor, ItemPhotoSrc = "http://www.pngmart.com/files/7/Armour-Download-PNG-Image.png", Price = 500 },
                    new Item{Name="Brauk", Power=20, ItemType=(int)Enums.ItemTypes.Armor, ItemPhotoSrc = "https://cdn3.iconfinder.com/data/icons/fantasy-and-role-play-game-adventure-quest/512/Potion-512.png", Price = 200 },
                    new Item{Name="Bud", Power=50, ItemType=(int)Enums.ItemTypes.Elixir, ItemSubType = 1, ItemPhotoSrc = "https://cdn3.iconfinder.com/data/icons/magic-show-1/100/magic_show-15-512.png", Price = 500 },
                    new Item{Name="Dhollu", Power=20, ItemType=(int)Enums.ItemTypes.Elixir, ItemSubType = 2, ItemPhotoSrc = "https://cdn3.iconfinder.com/data/icons/fantasy-and-role-play-game-adventure-quest/512/Potion-512.png", Price = 100 },
                    new Item{Name="Von", Power=50, ItemType=(int)Enums.ItemTypes.RockHelper, ItemPhotoSrc = "https://www.pngarts.com/files/3/Ice-Axe-PNG-Free-Download.png", Price = 400 },
                    new Item{Name="Rhokvu", Power=20, ItemType=(int)Enums.ItemTypes.RockHelper, ItemPhotoSrc = "https://i-love-png.com/images/axe-2831900_960_720.png", Price = 100 },
                    new Item{Name="Grok", Power=50, ItemType=(int)Enums.ItemTypes.Sword, ItemPhotoSrc = "https://tr.rbxcdn.com/b533e302b0e9a08649477a9cfc323c38/420/420/Hat/Png", Price = 300 },
                    new Item{Name="Grurgehn", Power=20, ItemType=(int)Enums.ItemTypes.Sword, ItemPhotoSrc = "https://i.pinimg.com/originals/51/b7/87/51b787deed26dd03e9f25275b2938741.png", Price = 100 },
                    new Item{Name="Voggla", Power=50, ItemType=(int)Enums.ItemTypes.TreeHelper, ItemPhotoSrc = "https://www.stickpng.com/img/objects/ax/axe", Price = 400 },
                    new Item{Name="Ghoc", Power=20, ItemType=(int)Enums.ItemTypes.TreeHelper, ItemPhotoSrc = "https://i.pinimg.com/originals/56/a6/39/56a639598a6bc6247ee92fc63dd64d07.png", Price = 100 },
                    new Item{Name="Draalvum", Power=50, ItemType=(int)Enums.ItemTypes.WaterHelper, ItemPhotoSrc = "https://i.pinimg.com/originals/f8/7f/f5/f87ff5029ae3eb46299775d475425d96.png", Price = 600 },
                    new Item{Name="Braklun", Power=20, ItemType=(int)Enums.ItemTypes.WaterHelper, ItemPhotoSrc = "https://pngimage.net/wp-content/uploads/2018/06/tank-water-png-5.png", Price = 100 },
                });
                _db.SaveChanges();
            }
        }
    }
}

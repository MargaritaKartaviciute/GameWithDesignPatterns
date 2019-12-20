using backend.Helpers;
using backend.Services.Visitor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class MapObject
    {
        [Key]
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        [ForeignKey("Map")]
        public int MapId { get; set; }

        public int Type { get; set; }
        //public virtual Map Map { get; set; }

        [NotMapped]
        public int CoinsWorthAmount
        {
            get
            {
                return this.CoinsWorth.CoinAmount();
            }
            set { }
        }
        [NotMapped]
        public MapObjectCoins CoinsWorth { get; set; }
        
        public void setCoinsWorth(MapObjectCoins worth)
        {
            CoinsWorth = worth;
        }

    }
}

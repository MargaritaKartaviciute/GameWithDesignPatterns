using backend.Helpers;
using backend.Models.MapObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Player : Prototype
    {   
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Score { get; set; }
        public int Energy { get; set; }
        public int Money { get; set; }
        public int LifeAmount { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public DateTime LastMove { get; set; }
        public int MovesCount { get; set; }
        public bool IsDead { get; set; } = false;
        [ForeignKey("Map")]
        public int? MapId { get; set; }

        public virtual List<PlayerItem> PlayerItems { get; set; }
        public virtual List<PlayerCutItems> PlayerCuttedItems { get; set; }

        public int CoinsAmount
        {
            get
            {
                if (PlayerCuttedItems == null) return 0;
                if(PlayerItems != null && PlayerItems.Count>0)
                    return PlayerCuttedItems.Select(a => a.CoinsWorth).Sum() - PlayerItems.Select(a => a.Item.Price).Sum();
                return PlayerCuttedItems.Select(a => a.CoinsWorth).Sum();
            }
        }

        public override Prototype Clone()
        {
            this.PlayerItems = PlayerItems.Select(a => {a.Player = this; return (PlayerItem)a.Clone(); }).ToList();
            return (Prototype)this.MemberwiseClone();
        }
    }

}

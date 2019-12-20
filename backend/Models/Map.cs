using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Map
    {
        public Map()
        {
            MapObjects = new HashSet<MapObject>();
            Players = new HashSet<Player>();
        }
        [Key]
        public int Id { get; set; }

        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public bool GameEnded { get; set; } = false;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [NotMapped]
        public string MovingPlayer
        {
            get
            {
                Player current = this.Players.OrderBy(a => a.LastMove).FirstOrDefault();
                return current != null ? current.UserName : "";
            }
        }
        public virtual ICollection<MapObject> MapObjects { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}

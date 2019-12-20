using backend.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class PlayerItem : Prototype
    {
        [Key]
        public int Id { get; set; }
        public Player Player { get; set; }
        public virtual int ItemId { get; set; }
        public Item Item { get; set; }

        public override Prototype Clone()
        {
            this.Id = 0;
            return (Prototype)this.MemberwiseClone();
        }
    }
}

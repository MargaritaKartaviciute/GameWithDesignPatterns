using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models.MapObjects
{
    public class PlayerCutItems
    {
        [Key]
        public int Id { get; set; }
        public int CoinsWorth {get;set;}
        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        //public virtual Player Player { get; set; }
    }
}
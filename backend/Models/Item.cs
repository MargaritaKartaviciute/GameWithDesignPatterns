using backend.Helpers;
using backend.Services.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int ItemType { get; set; }
        public int? ItemSubType { get; set; }
        public int Power { get; set; }
        public string ItemPhotoSrc { get; set; }
        [NotMapped]
        public IItemService service { get; set; }
    }
}

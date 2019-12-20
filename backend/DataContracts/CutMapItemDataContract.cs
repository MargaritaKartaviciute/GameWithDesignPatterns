using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.ViewModels
{
    public class CutMapItemDataContract
    {
        public int MapId { get; set; }
        public string Username { get; set; }
        public int MapObjectId { get; set; }
        public int ItemType { get; set; }
    }
}

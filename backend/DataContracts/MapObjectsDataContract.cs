using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DataContracts
{
    public class MapObjectsDataContract
    {
        public int Trees { get; set; }
        public int Rocks { get; set; }
        public int Water { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public string Username { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Helpers;
using backend.Models;

namespace backend.Services.MapObjects
{
    public class WaterService : IMapObjectsService
    {
        private readonly SalaContext _context;
        public WaterService(SalaContext context)
        {
            _context = context;
        }
        public MapObject Create(MapObject mapObject)
        {
            Console.WriteLine("Water creation");
            var water = new Water()
            {
                X = mapObject.X,
                Y = mapObject.Y,
                MapId = mapObject.MapId,
                Type = (int)Enums.MapObjectTypes.Water
            };
            _context.Water.Add(water);
            _context.SaveChanges();
            return water;
        }
    }
}

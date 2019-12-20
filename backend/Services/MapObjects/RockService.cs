using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Helpers;
using backend.Models;

namespace backend.Services.MapObjects
{
    public class RockService : IMapObjectsService
    {
        private readonly SalaContext _context;
        public RockService(SalaContext context)
        {
            _context = context;
        }
        public MapObject Create(MapObject mapObject)
        {
            Console.WriteLine("Rock creation");
            var rock = new RockBuilder().At(mapObject.X, mapObject.Y).SetMap(mapObject.MapId).Build();
            _context.Rock.Add(rock);
            _context.SaveChanges();
            return rock;
        }
    }
}

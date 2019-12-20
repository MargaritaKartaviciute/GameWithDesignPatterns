using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Helpers;
using backend.Models;

namespace backend.Services.MapObjects
{
    public class TreeService : IMapObjectsService
    {
        private readonly SalaContext _context;
        public TreeService(SalaContext context)
        {
            _context = context;
        }
        public MapObject Create(MapObject mapObject)
        {
            Console.WriteLine("Tree creation");
            var tree = new Tree()
            {
                X = mapObject.X,
                Y = mapObject.Y,
                MapId = mapObject.MapId,
                Type = (int)Enums.MapObjectTypes.Tree
            };
            _context.Tree.Add(tree);
            _context.SaveChanges();
            return tree;
        }
    }
}

using backend.Data;
using backend.Helpers;
using backend.Models;
using backend.Models.MapObjects;
using backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.MapObjects
{
    public class MapObjectsFactory: IMapObjectsFactory
    {
        private readonly SalaContext _context;
        public MapObjectsFactory(SalaContext context)
        {
            _context = context;
        }
        public MapObject CreateObject(Enums.MapObjectTypes type, MapObject mapObject)
        {
            MapObject createdObject = null;
            switch (type)
            {
                case Enums.MapObjectTypes.Tree:
                    createdObject = new TreeService(_context).Create(mapObject);
                    break;
                case Enums.MapObjectTypes.Rock:
                    createdObject = new RockService(_context).Create(mapObject);
                    break;
                case Enums.MapObjectTypes.Water:
                    createdObject = new WaterService(_context).Create(mapObject);
                    break;
            }

            return createdObject;
        }
    }
}

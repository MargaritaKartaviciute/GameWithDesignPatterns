using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.MapObjects
{
    public interface IMapObjectsService
    {
        MapObject Create(MapObject mapObject);
    }
}

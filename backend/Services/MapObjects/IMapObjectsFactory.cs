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
    public  interface IMapObjectsFactory
    {
        MapObject CreateObject(Enums.MapObjectTypes type, MapObject mapObject);
    }
}

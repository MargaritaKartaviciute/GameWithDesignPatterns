using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DataContracts;
using backend.Helpers;
using backend.Models;

namespace backend.Services.Maps
{
    public interface IMapService
    {
        Message Create(MapObjectsDataContract mapObject);
        Message Get();

        Message DeleteGame();
    }
}

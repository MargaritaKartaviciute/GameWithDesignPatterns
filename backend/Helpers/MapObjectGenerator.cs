using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DataContracts;
using backend.Migrations;
using backend.Models;
using backend.Services.Iterator;
using backend.Services.MapObjects;

namespace backend.Helpers
{
    public static class MapObjectGenerator
    {
        public static Map GenerateObjectCoordinates(Map map, MapObjectsDataContract mapObjects, IMapObjectsFactory _mapObjectFactory)
        {
            Random rand =  new Random();
            while (mapObjects.Trees > 0)
            {
                int x = rand.Next(1, map.MaxX);
                int y = rand.Next(1, map.MaxY);
                if (!CheckCoordinates(x, y, map))
                {
                    var mapObject = new MapObject
                    {
                        X = x,
                        Y = y,
                        MapId = map.Id
                    };
                    _mapObjectFactory.CreateObject(Enums.MapObjectTypes.Tree, mapObject);
                    mapObjects.Trees--;
                }
            }
            while (mapObjects.Rocks > 0)
            {
                int x = rand.Next(1, map.MaxX);
                int y = rand.Next(1, map.MaxY);
                if (!CheckCoordinates(x, y, map))
                {
                    var mapObject = new MapObject
                    {
                        X = x,
                        Y = y,
                        MapId = map.Id
                    };

                    _mapObjectFactory.CreateObject(Enums.MapObjectTypes.Rock, mapObject);
                    mapObjects.Rocks--;
                }
            }
            while (mapObjects.Water > 0)
            {
                int x = rand.Next(1, map.MaxX);
                int y = rand.Next(1, map.MaxY);
                if (!CheckCoordinates(x, y, map))
                {
                    var mapObject = new MapObject
                    {
                        X = x,
                        Y = y,
                        MapId = map.Id
                    };
                    _mapObjectFactory.CreateObject(Enums.MapObjectTypes.Water, mapObject);
                    mapObjects.Water--;
                }   
            }

            return map;
        }
        public static bool CheckCoordinates(int x, int y, Map map)
        {
            IIterator<MapObject> iter = new Iterator<MapObject>(map.MapObjects.ToArray());
            while (iter.HasNext())
            {
                var item = iter.Next();
                if (item.X == x && item.Y == y)
                {
                    return true;
                }
            }
            return false;
        }

        public static Player PlayerCoordinates(Map map, Player player)
        {
            Random rand = new Random();
            int x = rand.Next(1, map.MaxX);
            int y = rand.Next(1, map.MaxY);
            while (player.X == 0 && player.Y == 0)
            {
                if (!CheckCoordinates(x, y, map))
                {
                    player.X = x;
                    player.Y = y;
                    return player;
                }
            }

            return null;
        }
        public static bool CanPlayerMove(int currentX, int currentY, int x, int y)
        {
            if( currentX == x)
            {
                if (y == currentY + 1)
                    return true;
                if (y == currentY - 1)
                    return true;
            }

            if (currentY == y)
            {
                if (x == currentX + 1)
                    return true;
                if (x == currentX - 1)
                    return true;
            }
            return false;
        }

        public static int CheckCoordinatesForPlayer(int x, int y, Map map)
        {
            foreach (var item in map.MapObjects)
            {
                if (item.X == x && item.Y == y)
                    return item.Id;
            }
            return -1;
        }
        public static int CheckCoordinatesForEnemy(int x, int y, Player player, Map map)
        {
            foreach (var item in map.Players)
            {
                if (item.X == x && item.Y == y && item.Id != player.Id)
                    return item.Id;
            }
            return -1;
        }
        static public void AddNewObjects(Map map)
        {
            Random rand = new Random();
            for (int i = 0; i < rand.Next(1, 2); i++)
            {
                int maxIterations = 5;
                bool objPlaceFound = false;
                while (!objPlaceFound && maxIterations > 0)
                {
                    int x = rand.Next(1, map.MaxX);
                    int y = rand.Next(1, map.MaxY);
                    if (!MapObjectGenerator.CheckCoordinates(x, y, map) && !map.Players.Any(a => a.X == x && a.Y == y))
                    {
                        objPlaceFound = true;
                        map.MapObjects.Add(getMapObjectToAdd(map, x, y, rand.Next(1, 3)));
                    }
                    else maxIterations--;
                }
            }
        }
        static private MapObject getMapObjectToAdd(Map map, int x, int y, int type)
        {
            switch (type)
            {
                case 1: return new Water { MapId = map.Id, Type = (int)Enums.MapObjectTypes.Water, X = x, Y = y };
                case 2: return new Tree { MapId = map.Id, Type = (int)Enums.MapObjectTypes.Tree, X = x, Y = y };
                case 3: return new Rock { MapId = map.Id, Type = (int)Enums.MapObjectTypes.Rock, X = x, Y = y };
            }
            return null;
        }
    }
}

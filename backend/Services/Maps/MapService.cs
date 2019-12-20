using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.DataContracts;
using backend.Helpers;
using backend.Models;
using backend.Services.Flyweight;
using backend.Services.MapObjects;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Maps
{
    public class MapService: IMapService
    {
        private readonly SalaContext _context;
        private readonly IMapObjectsFactory _mapObjectFactory;
        private string MessageText = null;
        public MapService(SalaContext context, IMapObjectsFactory mapObjectFactory)
        {
            _context = context;
            _mapObjectFactory = mapObjectFactory;
        }

        public Message Create(MapObjectsDataContract mapObject)
        {
            try
            {
                ProgramMeasurement p = ProgramMeasurementFactory.GetMeasurementType("Memory");
                ProgramMeasurement s = ProgramMeasurementFactory.GetMeasurementType("Speed");
                p.Start();
                s.Start();
                var map = new Map
                {
                    MaxX = mapObject.MaxX,
                    MaxY = mapObject.MaxY
                };
                _context.Add(map);
                _context.SaveChanges();

                map = MapObjectGenerator.GenerateObjectCoordinates(map, mapObject, _mapObjectFactory);
                p.Stop();
                s.Stop();
                p.Measure();
                s.Measure();
                return new Message();
            }
            catch(Exception e)
            {
                return new Message { IsValid = false, MessageText = e.Message };
            }
            
        }

        public Message DeleteGame()
        {
            if (CheckIfMapDeletable())
            {
                Map map = _context.Map.OrderByDescending(a => a.CreateDate).FirstOrDefault();
                map.GameEnded = true;
                _context.SaveChanges();
                return new Message { IsValid = true };
            }
            return new Message
            {
                IsValid = false,
                MessageText = this.MessageText
            };
        }

        private bool CheckIfMapDeletable()
        {
            if (_context.Map.Any() && _context.Map.OrderByDescending(a => a.CreateDate).First().GameEnded) { MessageText = "Game already ended"; return false; }
            if (_context.Player.Any(a => a.MovesCount >= 50)) return true;
            if (_context.Player.Where(a => a.LifeAmount > 0).Count() == 1) return true;
            MessageText = "Moves count more than 0 and more than 1 player still alive";
            return false;
        }

        public Message Get()
        {
            if (_context.Map.Any(a => !a.GameEnded)) return new Message();
            return new Message { IsNotFound = true };
        }
    }
}

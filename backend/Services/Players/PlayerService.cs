using backend.Data;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using backend.Helpers;
using Microsoft.EntityFrameworkCore;
using backend.ViewModels;
using backend.Models.MapObjects;
using backend.Services.Adapter;
using backend.Services.Command;
using backend.Services.Template;
using backend.Services.State;

namespace backend.Services.Players
{
    public class PlayerService: IPlayerService
    {
        private readonly SalaContext _context;
        private StateContext _state;
        public PlayerService(SalaContext context)
        {
            _context = context;
            _state = new StateContext(_context);
        }

        private static PlayerService instance = null;
        private static object threadLock = new object();
        public static PlayerService getInstance()
        {
                if(instance == null)
                {
                    Debug.WriteLine("kuriame nauja instance");
                    instance = new PlayerService(SalaContext.getInstance());
                }
                else
                {
                    Debug.WriteLine("nekuriame naujo instance nes jis jau sukurtas");
                    instance = new PlayerService(SalaContext.getInstance());
                }
                return instance;
        }

        public Player GetById(int id)
        {
            
            if (_context.Player.Any())
            {
                return _context.Player
                              .FirstOrDefault(x => x.Id == id);
            }
            return null;
        }
        public bool DeleteById(int id)
        {
            var item = _context.Player
                .SingleOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.Player.Remove(item);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public Message Add(Player newItem)
        {
            var map = _context.Map.Include(a=>a.Players).OrderByDescending(a => a.Id).FirstOrDefault();
            if (map.Players.Any(a => a.UserName == newItem.UserName)) return new Message { IsValid = false, MessageText = "username already exists" };
            if (newItem.UserName.Contains("_ghost"))
            {
                string username = newItem.UserName.Split("_")[0];
                Player clonablePlayer = _context.Player.AsNoTracking().Where(a => a.UserName == username && a.MapId == map.Id).Include(a=>a.PlayerItems).FirstOrDefault();
                if(clonablePlayer != null)
                {
                    Player cloned = (Player)clonablePlayer.Clone();
                    Tuple<int, int> newCoordinates = getEmptySpot(clonablePlayer);
                    if(newCoordinates != null)
                    {
                        cloned.Id = 0;
                        cloned.MapId = map.Id;
                        cloned.X = newCoordinates.Item1;
                        cloned.Y = newCoordinates.Item2;
                        cloned.UserName = newItem.UserName;
                        cloned.LastMove = DateTime.Now;
                        Player adasd = new Player();
                        List<PlayerItem> items = cloned.PlayerItems.Select(a => { a.Item = _context.Item.FirstOrDefault(x => x.Id == a.ItemId); return a; }).ToList();
                        cloned.PlayerItems = null;
                        _context.Player.Add(cloned);
                        _context.SaveChanges();
                        cloned.PlayerItems = items.Select(a=> { a.Player = cloned; return a; }).ToList();
                        _context.PlayerItems.AddRange(cloned.PlayerItems);
                        _context.SaveChanges();
                        _context.PlayerCutItem.Add(new PlayerCutItems { PlayerId = cloned.Id, CoinsWorth =_context.PlayerCutItem.Where(a=>a.PlayerId == clonablePlayer.Id).Select(a=>a.CoinsWorth).Sum()  });
                        _context.SaveChanges();

                        return new Message();
                    }
                }
            }
            if (map != null)
            {
               newItem =  MapObjectGenerator.PlayerCoordinates(map, newItem);
            }
            if (newItem != null)
            {
                newItem.Score = 100;
                newItem.MapId = map.Id;
                newItem.LifeAmount = 100;
                newItem.Energy = 100;
                newItem.Money = 0;
                newItem.MovesCount = 50;
                newItem.LastMove = DateTime.Now;
                _context.Add(newItem);
                _context.SaveChanges();
                return new Message();
            }
            return null;

        }

        private Tuple<int, int> getEmptySpot(Player clonablePlayer)
        {
            Map map = _context.Map.FirstOrDefault(a => a.Id == clonablePlayer.MapId);
            if (map != null)
            {
                Random rnd = new Random();
                while (true)
                {
                    int newX = rnd.Next(1, map.MaxX);
                    int newY = rnd.Next(1, map.MaxY);
                    if(!_context.Tree.Any(a=>a.X == newX && a.Y == newY)
                        && !_context.Rock.Any(a => a.X == newX && a.Y == newY)
                        && !_context.Water.Any(a => a.X == newX && a.Y == newY))
                    {
                        return new Tuple<int, int>(newX, newY);
                    }
                }
            }
            return null;
        }

        public Message Move(string username, Player newUpdate)
        {
            if (_context.Map.OrderByDescending(a=>a.CreateDate).Any() && _context.Map.Include(a=>a.Players).OrderByDescending(a => a.CreateDate).First().Players.OrderBy(a=>a.LastMove).First().UserName != username)
            {
                return new Message { IsValid=false, MessageText="Other player is moving, please wait for your turn"};
            }
            var player = _context.Player.FirstOrDefault(x => x.UserName == username 
                                                            && _context.Map.Any(a=>a.Id == x.MapId 
                                                                                && a.Players.OrderBy(z=>z.LastMove).First().UserName == username));
            if (player != null)
            {
                var map = _context.Map.Include(x => x.MapObjects)
                                       .Include(x => x.Players)
                                       .FirstOrDefault(x => x.Id == player.MapId);
                if (map != null)
                {
                    var canMove = MapObjectGenerator.CanPlayerMove(player.X, player.Y, newUpdate.X, newUpdate.Y);
                    if (!canMove)
                    {
                        return new Message { IsValid=false, MessageText="Player is not able to move there" };
                    }
                    
                    var objectId = MapObjectGenerator.CheckCoordinatesForPlayer(newUpdate.X, newUpdate.Y, map);
                    if (objectId > 0)
                    {
                        if (_context.Tree.Any(x => x.Id == objectId))
                        {
                            _state.SetState(new TreeCutMove());
                        }
                        else if (_context.Rock.Any(x => x.Id == objectId))
                        {
                            _state.SetState(new RockCutMove());
                        }
                        else if (_context.Water.Any(x => x.Id == objectId))
                        {
                            _state.SetState(new WaterCutMove());
                        }
                    }
                    var enemyId = MapObjectGenerator.CheckCoordinatesForEnemy(newUpdate.X, newUpdate.Y, player, map);
                    if (enemyId > 0)
                    {
                        _state.SetState( new CombatMove());
                    }
                    _state.MakeMovement(player, newUpdate, map, objectId, enemyId);
                }
                
                return new Message();
            }
            return new Message { IsValid = false, MessageText = "Player not found" };
        }

        public void addEnergy(string playerName, int amount)
        {
            var player = _context.Player.Where(e => e.UserName == playerName).FirstOrDefault();
            if (player != null)
            {
                player.Energy += amount;
            }
            _context.SaveChanges();
        }

        public void addMoney(string playerName, int amount)
        {
            var player = _context.Player.Where(e => e.UserName == playerName).FirstOrDefault();
            if (player != null)
            {
                player.Money += amount;
            }
            _context.SaveChanges();
        }

        public void addLife(string playerName, int amount)
        {
            var player = _context.Player.Where(e => e.UserName == playerName).FirstOrDefault();
            if (player != null)
            {
                player.LifeAmount += amount;
            }
            _context.SaveChanges();
        }
    }
}

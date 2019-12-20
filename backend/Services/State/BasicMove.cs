using backend.Data;
using backend.Helpers;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.State
{
    public class BasicMove: IMove
    {
        public void Move(Player player, Player newUpdate, SalaContext _context, Map map, int id, int enemyId)
        {
            player.LastMove = DateTime.Now;
            player.X = newUpdate.X;
            player.Y = newUpdate.Y;
            player.MovesCount -= 1;

            MapObjectGenerator.AddNewObjects(map);

            _context.SaveChanges();
        }
    }
}

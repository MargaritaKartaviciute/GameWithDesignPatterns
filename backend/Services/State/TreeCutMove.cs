using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Helpers;
using backend.Models;
using backend.Services.Template;

namespace backend.Services.State
{
    public class TreeCutMove : IMove
    {
        private PlayerMovementTemplate _template;
        public void Move(Player player, Player newUpdate, SalaContext _context, Map map, int id, int enemyId)
        {
            _template = new ConfrontationWithTree(_context);
            _template.MakeMovement(player.UserName, id);

            player.LastMove = DateTime.Now;
            player.X = newUpdate.X;
            player.Y = newUpdate.Y;
            player.MovesCount -= 1;

            MapObjectGenerator.AddNewObjects(map);

            _context.SaveChanges();
        }
    }
}

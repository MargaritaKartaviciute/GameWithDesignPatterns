using backend.Data;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.State
{
    public interface IMove
    {
        void Move(Player player, Player newUpdate, SalaContext _context, Map map, int id, int enemyId);
    }
}

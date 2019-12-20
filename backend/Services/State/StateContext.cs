using backend.Data;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.State
{
    public class StateContext
    {
        private IMove _currentMove;
        private SalaContext _context;

        public StateContext(SalaContext context)
        {
            _currentMove = new BasicMove();
            _context = context;
        }
        public void SetState(IMove move)
        {
            _currentMove = move;
        }
        public void MakeMovement(Player player, Player newUpdate, Map map, int id, int enemyId)
        {
            _currentMove.Move(player, newUpdate, _context, map, id, enemyId);
        }
    }
}

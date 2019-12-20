using backend.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.PlayerMatches
{
    public class PlayerMatchService: IPlayerMatchService
    {
        private readonly SalaContext _context;
        public PlayerMatchService(SalaContext context)
        {
            _context = context;
        }
    }
}

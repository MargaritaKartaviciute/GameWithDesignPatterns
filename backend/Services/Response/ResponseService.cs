using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Response
{
    public class ResponseService : IResponseService
    {
        private readonly SalaContext _context;
        public ResponseService(SalaContext context)
        {
            _context = context;
        }

        public Map GetResponse()
        {
            
            if (_context.Map.Any(a => !a.GameEnded))
            {
                var query = _context.Map.Where(a => !a.GameEnded);
                if (query.Any(a => a.Players != null && a.Players.Count() > 0)) 
                {
                    query = query.Include(a => a.Players);
                    if (query.Any(a => a.Players.Any(x => x.PlayerItems != null && x.PlayerItems.Count() > 0)))
                        query = query.Include(a => a.Players).ThenInclude(x => x.PlayerItems).ThenInclude(y => y.Item);
                    if (query.Any(a => a.Players.Any(x => x.PlayerCuttedItems != null && x.PlayerCuttedItems.Count() > 0)))
                        query = query.Include(x => x.Players).ThenInclude(y => y.PlayerCuttedItems);
                }
                if (query.Any(a => a.MapObjects != null && a.MapObjects.Count() > 0)) query = query.Include(a => a.MapObjects);
                return query.OrderByDescending(a=>a.CreateDate).FirstOrDefault();
            }
            return null;
        }
    }
}

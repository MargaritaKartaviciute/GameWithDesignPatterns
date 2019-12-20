using backend.Helpers;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.PlayerItems
{
    public interface IPlayerItemsService
    {
        Message GetItemsByUser(string username);
        Message AddPlayerItem(string username, int itemId);
        bool DeletePlayerItem(int id);
    }
}

using backend.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Hubs
{
    public class MapHub: Hub
    {
        public async Task SendCreatedMap(Map map)
        {
            await Clients.Others.SendAsync("ReceiveCreatedMap", map);
        }
    }
}

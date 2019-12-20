using backend.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Hubs
{
    public class PlayerHub: Hub
    {
        //!!! DEMESIO Pavyzdys, reikes pakeisti pagal reikiama logika
        public async Task SendCreatedPlayer(Player player)
        {
            await Clients.Others.SendAsync("ReceiveCreatedPlayer", player);
        }
    }
}

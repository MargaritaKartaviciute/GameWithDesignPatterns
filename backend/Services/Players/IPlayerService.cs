using backend.Helpers;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Players
{
    public interface IPlayerService
    {
        Player GetById(int id);
        Message Add(Player newItem);
        bool DeleteById(int id);
        Message Move(string username, Player newUpdate);
        void addEnergy(string playerName, int amount);
        void addMoney(string playerName, int amount);
        void addLife(string playerName, int amount);
    }
}

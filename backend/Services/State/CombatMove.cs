using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Helpers;
using backend.Models;

namespace backend.Services.State
{
    public class CombatMove : IMove
    {
        //Pakeisti pagal turimus items, turi keistis ir kieks atimama energijos bei sveikatos
        private static int EnergyDeprivator = 20;
        private static int HealthDeprivator = 10;
        public void Move(Player player, Player newUpdate, SalaContext _context, Map map, int id, int enemyId)
        {
            var enemy = _context.Player.FirstOrDefault(x => x.Id == enemyId);
            
            if (enemy != null)
                enemy.LifeAmount -= HealthDeprivator;

            player.LastMove = DateTime.Now;
            player.MovesCount -= 1;
            player.Energy -= EnergyDeprivator;
            
            MapObjectGenerator.AddNewObjects(map);
            _context.SaveChanges();
        }
    }
}

using backend.Services.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Interpreter
{
    public class InterpreterContext
    {
        public string playerName;
        public string command;
        public int amount;
        private IPlayerService playerService;

        public InterpreterContext(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        public string Execute()
        {
            if (amount == 0)
            {
                amount = 10;
            }
            switch (command.Trim())
            {
                case "give_energy":
                    playerService.addEnergy(playerName, amount);
                    return "Gave " + amount + " energy to " + playerName;
                case "give_money":
                    playerService.addMoney(playerName, amount);
                    return "Gave " + amount + " money to " + playerName;
                case "give_life":
                    playerService.addLife(playerName, amount);
                    return "Gave " + amount + " life to " + playerName;
            }
            return "No action";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Command
{
    public class RockScores: ICommand
    {
        private Receiver _receiver;
        public RockScores(Receiver receiver)
        {
            _receiver = receiver;
        }

        public void Execute(string username)
        {
            _receiver.ChangePlayerEnergyWithRock(username);
            _receiver.RecalculatePlayerMoney(username);
        }

        public void Undo(string username)
        {
            _receiver.UndoPlayerEnergyWithRockChanges(username);
            _receiver.UndoPlayerMoneyRecalculation(username);
        }
    }
}

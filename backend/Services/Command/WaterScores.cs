using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Command
{
    public class WaterScores: ICommand
    {
        private Receiver _receiver;
        public WaterScores(Receiver receiver)
        {
            _receiver = receiver;
        }

        public void Execute(string username)
        {
            _receiver.ChangePlayerEnergyWithWater(username);
            _receiver.RecalculatePlayerMoney(username);
        }
        public void Undo(string username)
        {
            _receiver.UndoPlayerEnergyWithWaterChanges(username);
            _receiver.UndoPlayerMoneyRecalculation(username);
        }
    }
}

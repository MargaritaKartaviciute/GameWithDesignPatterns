using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Command
{
    public class TreeScores : ICommand
    {
        private Receiver _receiver;
        public TreeScores(Receiver receiver)
        {
            _receiver = receiver;
        }
        public void Execute(string username)
        {
            _receiver.ChangePlayerEnergyWithTree(username);
            _receiver.RecalculatePlayerMoney(username);
        }
        public void Undo(string username)
        {
            _receiver.UndoPlayerEnergyWithTreeChanges(username);
            _receiver.UndoPlayerMoneyRecalculation(username);
        }
    }
}

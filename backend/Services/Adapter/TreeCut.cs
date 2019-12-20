using backend.Models;
using backend.Services.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Adapter
{
    public class TreeCut: ICutObjects
    {
        private readonly Adaptee _adaptee;
        public TreeCut(Adaptee adaptee)
        {
            _adaptee = adaptee;
        }
        public void Cut(string username, int objectId)
        {
            _adaptee.CutItemByType(username, new Tree(), objectId);
        }
    }
}

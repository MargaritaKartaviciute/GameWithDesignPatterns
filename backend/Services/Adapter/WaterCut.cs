using backend.Data;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Players
{
    public class WaterCut: ICutObjects
    {
        private readonly Adaptee _adaptee;
        public WaterCut(Adaptee adaptee)
        {
            _adaptee = adaptee;
        }
        public void Cut(string username, int objectId)
        {
            _adaptee.CutItemByType(username, new Water(), objectId);
        }
    }
}

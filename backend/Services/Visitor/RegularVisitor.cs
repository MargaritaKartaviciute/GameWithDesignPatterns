using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Services.Visitor
{
    public class RegularVisitor : Visitor
    {
        public int visit(Tree item)
        {
            return item.CoinsWorth.CoinAmount() * 1;
        }

        public int visit(Rock item)
        {
            return item.CoinsWorth.CoinAmount() * 1;
        }

        public int visit(Water item)
        {
            return item.CoinsWorth.CoinAmount() * 1;
        }
    }
}

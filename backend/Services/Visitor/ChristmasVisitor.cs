using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Services.Visitor
{
    public class ChristmasVisitor : Visitor
    {
        public int visit(Tree item)
        {
            return (int)Math.Ceiling(item.CoinsWorth.CoinAmount() * 1.12);
        }

        public int visit(Rock item)
        {
            return (int)Math.Ceiling(item.CoinsWorth.CoinAmount() * 1.15);
        }

        public int visit(Water item)
        {
            return (int)Math.Ceiling(item.CoinsWorth.CoinAmount() * 1.08);
        }
    }
}

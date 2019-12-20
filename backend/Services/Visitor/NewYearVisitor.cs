using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Services.Visitor
{
    public class NewYearVisitor : Visitor
    {
        public int visit(Tree item)
        {
            return (int)Math.Ceiling(item.CoinsWorth.CoinAmount() * 1.08);
        }

        public int visit(Rock item)
        {
            return (int)Math.Ceiling(item.CoinsWorth.CoinAmount() * 1.10);
        }

        public int visit(Water item)
        {
            return (int)Math.Ceiling(item.CoinsWorth.CoinAmount() * 1.05);
        }
    }
}

using backend.Helpers;
using backend.Services.Visitor;

namespace backend.Models
{
    public class Tree: MapObject, Visitable
    {
        public Tree()
        {
            CoinsWorth = new TreeCoinAmount();
        }
        public int accept(Visitor visitor)
        {
            return visitor.visit(this);
        }
    }
}

using backend.Helpers;
using backend.Services.Visitor;

namespace backend.Models
{
    public class Water: MapObject, Visitable
    {
        public Water()
        {
            CoinsWorth = new WaterCoinAmount();
        }
        public int accept(Visitor visitor)
        {
            return visitor.visit(this);
        }
    }
}

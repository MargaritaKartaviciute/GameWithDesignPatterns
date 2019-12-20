using backend.Helpers;
using backend.Services.Visitor;

namespace backend.Models
{
    public class RockBuilder : IBuilder<Rock>
    {
        private int X;
        private int Y;
        private int MapId;
        public RockBuilder SetMap(int mapId)
        {
            this.MapId = mapId;
            return this;
        }
        public RockBuilder At(int x, int y)
        {
            this.X = x;
            this.Y = y;
            return this;
        }
        public Rock Build()
        {
            Rock rock = new Rock
            {
                MapId = this.MapId,
                X = this.X,
                Y = this.Y,
                Type = (int)Enums.MapObjectTypes.Rock
            };
            return rock;
        }
    }
    public class Rock: MapObject, Visitable
    {
        public Rock()
        {
            CoinsWorth = new RockCoinAmount();
        }

        public int accept(Visitor visitor)
        {
            return visitor.visit(this);
        }
    }
}

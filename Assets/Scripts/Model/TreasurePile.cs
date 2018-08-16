using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class TreasurePile
    {
        public int          X           { get; set; }
        public int          Y           { get; set; }
        public ITreasure    Treasure    { get; set; }

        public TreasurePile(int x, int y, ITreasure treasure)
        {
            X = x;
            Y = y;
            Treasure = treasure;

            IDrawable drawableTreasure = treasure as IDrawable;

            if (drawableTreasure != null)
            {
                drawableTreasure.X = x;
                drawableTreasure.Y = y;
            }
        }
    }
}
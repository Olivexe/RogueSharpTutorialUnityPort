using RogueSharp;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class Gold : ITreasure, IDrawable
    {
        public int      Amount  { get; set; }
        public Colors   Color   { get; set; }
        public char     Symbol  { get; set; }
        public int      X       { get; set; }
        public int      Y       { get; set; }

        private Game    game;

        public Gold(int amount, Game game)
        {
            Amount      = amount;
            Symbol      = '$';
            Color       = Colors.Yellow;
            this.game   = game;
        }

        public string GetName()
        {
            return Amount + " Gold";
        }

        public bool PickUp(Actor actor)
        {
            actor.Gold += Amount;
            game.MessageLog.Add(actor.Name + " picked up " + Amount + " gold");
            return true;
        }

        public void Draw(IMap map)
        {
            if (!map.IsExplored(X, Y))
            {
                return;
            }

            if (map.IsInFov(X, Y))
            {
                game.SetMapCell(X, Y, Color, Colors.FloorBackground, Symbol, map.GetCell(X, Y).IsExplored);
            }
            else
            {
                game.SetMapCell(X, Y, Colors.YellowGray, Colors.FloorBackground, Symbol, map.GetCell(X, Y).IsExplored);
            }
        }
    }
}

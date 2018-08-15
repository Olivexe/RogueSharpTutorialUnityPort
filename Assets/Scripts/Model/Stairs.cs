using RogueSharp;
using RogueSharpTutorial.View;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class Stairs : IDrawable
    {
        public Colors   Color           { get; set; }
        public bool     IsUp            { get; set; }
        public char     Symbol          { get; set; }
        public int      X               { get; set; }
        public int      Y               { get; set; }

        private Game game;

        public Stairs(Game game)
        {
            this.game = game;
        }

        public void Draw(IMap map)
        {
            if (!map.GetCell(X, Y).IsExplored)
            {
                return;
            }

            Symbol = IsUp ? '<' : '>';

            if (map.IsInFov(X, Y))
            {
                Color = Colors.Player;
            }
            else
            {
                Color = Colors.Floor;
            }

            game.SetMapCell(X, Y, Color, Colors.FloorBackground, Symbol, map.GetCell(X, Y).IsExplored);
        }
    }
}
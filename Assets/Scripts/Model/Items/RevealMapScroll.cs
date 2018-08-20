using RogueSharp;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class RevealMapScroll : Item
    {
        public RevealMapScroll(Game game) : base(game)
        {
            Name = "Magic Map";
            RemainingUses = 1;
        }

        protected override bool UseItem()
        {
            DungeonMap map = game.World;

            game.MessageLog.Add($"{game.Player.Name} reads a {Name} and gains knowledge of the surrounding area");

            foreach (Cell cell in map.GetAllCells())
            {
                if (cell.IsWalkable)
                {
                    map.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }

            RemainingUses--;

            return true;
        }
    }
}

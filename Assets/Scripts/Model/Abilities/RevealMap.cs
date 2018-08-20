using System;
using RogueSharpTutorial.Controller;

using RogueSharp;

namespace RogueSharpTutorial.Model
{
    public class RevealMap : Ability
    {
        private readonly int revealDistance;

        public RevealMap(Game game, int revealDistance) : base (game)
        {
            Name                = "Reveal Map";
            TurnsToRefresh      = 100;
            TurnsUntilRefreshed = 0;
            this.revealDistance = revealDistance;
        }

        protected override bool PerformAbility()
        {
            DungeonMap map = game.World;
            Player player = game.Player;

            foreach (Cell cell in map.GetCellsInSquare(player.X, player.Y, revealDistance))
            {
                if (cell.IsWalkable)
                {
                    map.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }

            return true;
        }
    }
}
using RogueSharp;
using RogueSharpTutorial.Model.Interfaces;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class SplitOoze : IBehavior
    {
        public bool Act(Actor actor, Game game)
        {
            DungeonMap map = game.World;

            // Ooze only splits when wounded
            if (actor.Health >= actor.MaxHealth)
            {
                return false;
            }

            int halfHealth = actor.MaxHealth / 2;
            if (halfHealth <= 0)
            {
                // Health would be too low so bail out
                return false;
            }

            Cell cell = FindClosestUnoccupiedCell(map, actor.X, actor.Y);

            if (cell == null)
            {
                // No empty cells so bail out
                return false;
            }

            // Make a new ooze with half the health of the old one
            Ooze newOoze = Monster.Clone(game, actor as Monster) as Ooze;

            if (newOoze != null)
            {
                newOoze.TurnsAlerted = 1;
                newOoze.X = cell.X;
                newOoze.Y = cell.Y;
                newOoze.MaxHealth = halfHealth;
                newOoze.Health = halfHealth;
                map.AddMonster(newOoze);
                game.MessageLog.Add($"{actor.Name} splits itself in two");
            }
            else
            {
                // Not an ooze so bail out
                return false;
            }

            // Halve the original ooze's health too
            actor.MaxHealth = halfHealth;
            actor.Health = halfHealth;

            return true;
        }

        private Cell FindClosestUnoccupiedCell(DungeonMap dungeonMap, int x, int y)
        {
            for (int i = 1; i < 5; i++)
            {
                foreach (Cell cell in dungeonMap.GetBorderCellsInSquare(x, y, i))
                {
                    if (cell.IsWalkable)
                    {
                        return cell;
                    }
                }
            }

            return null;
        }
    }
}

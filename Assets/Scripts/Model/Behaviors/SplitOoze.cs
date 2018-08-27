using RogueSharp;
using RogueSharpTutorial.Utilities;
using RogueSharpTutorial.Model.Interfaces;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class SplitOoze : IBehavior
    {
        public Actor        Parent  { get; private set; }
        public Game         Game    { get; private set; }
        public DungeonMap   World   { get; private set; }

        public void SetBehavior(Game game, Actor parent)
        {
            Game = game;
            Parent = parent;
            World = game.World;
        }

        public bool Act()
        {
            if (Parent.Health >= Parent.MaxHealth)                          // Ooze only splits when wounded
            {
                return false;
            }

            int halfHealth = Parent.MaxHealth / 2;
            if (halfHealth <= 0)
            {
                return false;                                               // Health would be too low so bail out
            }

            Cell cell = FindClosestUnoccupiedCell(World, Parent.X, Parent.Y);

            if (cell == null)
            {
                return false;                                               // No empty cells so bail out
            }

            Monster newOoze = ActorGenerator.CreateMonster(MonsterList.ooze, Game, Game.mapLevel, new Point(cell.X, cell.Y));

            if (newOoze != null)
            {
                newOoze.TurnsAlerted = 1;
                newOoze.MaxHealth = halfHealth;
                newOoze.Health = halfHealth;
                World.AddMonster(newOoze);
                newOoze.SetMapAwareness();
                newOoze.SetBehavior();
                Game.MessageLog.Add($"{Parent.Name} splits itself in two");
            }
            else
            {
                return false;                                               // Not an ooze so bail out
            }

            Parent.MaxHealth = halfHealth;                                  // Halve the original ooze's health too       
            Parent.Health = halfHealth;

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

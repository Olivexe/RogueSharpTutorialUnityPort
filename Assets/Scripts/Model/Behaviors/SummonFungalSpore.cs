using RogueSharp;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Utilities;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class SummonFungalSpore : SummonMonster
    {
        public new bool Summon()
        {
            for(int x = 0; x < numberToSummon; x++)
            {
                Cell cell = FindClosestUnoccupiedCell(World, Parent.X, Parent.Y);

                if (cell == null)
                {
                    break;// No empty cells so bail out
                }

                // Make a new ooze with half the health of the old one
                Monster monster = ActorGenerator.CreateMonster(monsterToSummon, Game, Game.mapLevel, new Point(cell.X, cell.Y));
                monster.SetMapAwareness();
                monster.SetBehavior();

                if (monster != null)
                {
                    monster.TurnsAlerted = 1;
                    World.AddMonster(monster);
                    Game.MessageLog.Add($"A {monster.Name} pops off the {Parent.Name}.");
                }
            }

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
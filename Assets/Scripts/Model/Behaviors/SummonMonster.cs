using RogueSharp;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Utilities;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class SummonMonster : IBehavior
    {
        private MonsterList monsterToSummon;
        private int         numberToSummon;

        public Actor       Parent  { get; private set; }
        public Game        Game    { get; private set; }
        public DungeonMap  World   { get; private set; }

        public void SetBehavior(Game game, Actor parent, MonsterList monsterToSummon, int numberToSummon)
        {
            Game                = game;
            Parent              = parent;
            World               = game.World;
            this.monsterToSummon= monsterToSummon;
            this.numberToSummon = numberToSummon;
        }

        public bool Act()
        {
            return Summon();
        }

        public bool Summon()
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

                if (monster != null)
                {
                    monster.TurnsAlerted = 1;
                    World.AddMonster(monster);
                    Game.MessageLog.Add($"{Parent.Name} summons a {monster.Name}");
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
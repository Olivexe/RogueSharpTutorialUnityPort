using RogueSharp;
using RogueSharp.Random;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class DestructionWand : Item
    {
        public DestructionWand(Game game) : base(game)
        {
            Name = "Wand of Destruction";
            RemainingUses = 3;
        }

        protected override bool UseItem()
        {
            DungeonMap map  = game.World;
            Player player   = game.Player;
            Point edgePoint = GetRandomEdgePoint(map);

            game.MessageLog.Add($"{player.Name} uses a {Name} and chaotically unleashes a void beam");

            Actor voidAttackActor = new Actor(game)
            {
                AttackBase = 6,
                AttackChanceBase = 90,
                Name = "The Void"
            };

            Cell previousCell = null;

            foreach (Cell cell in map.GetCellsAlongLine(player.X, player.Y, edgePoint.X, edgePoint.Y))
            {
                if (cell.X == player.X && cell.Y == player.Y)
                {
                    continue;
                }

                Monster monster = map.GetMonsterAt(cell.X, cell.Y);

                if (monster != null)
                {
                    Command.Attack(voidAttackActor, monster, true);
                }
                else
                {
                    map.SetCellProperties(cell.X, cell.Y, true, true, true);
                    if (previousCell != null)
                    {
                        if (cell.X != previousCell.X || cell.Y != previousCell.Y)
                        {
                            map.SetCellProperties(cell.X + 1, cell.Y, true, true, true);
                        }
                    }
                    previousCell = cell;
                }
            }

            RemainingUses--;

            return true;
        }

        private Point GetRandomEdgePoint(DungeonMap map)
        {
            var random = new DotNetRandom();
            int result = random.Next(1, 4);
            switch (result)
            {
                case 1: // TOP
                    {
                        return new Point(random.Next(3, map.Width - 3), 3);
                    }
                case 2: // BOTTOM
                    {
                        return new Point(random.Next(3, map.Width - 3), map.Height - 3);
                    }
                case 3: // RIGHT
                    {
                        return new Point(map.Width - 3, random.Next(3, map.Height - 3));
                    }
                case 4: // LEFT
                    {
                        return new Point(3, random.Next(3, map.Height - 3));
                    }
                default:
                    {
                        return new Point(3, 3);
                    }
            }
        }
    }
}


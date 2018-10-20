using System.Collections.Generic;
using RogueSharp;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class Whirlwind : Ability
    {
        public Whirlwind(Game game, Actor abilityOwner) : base(game, abilityOwner)
        {
            Name = "Whirlwind";
            TurnsToRefresh = 20;
            TurnsUntilRefreshed = 0;
        }

        protected override bool PerformAbility()
        {
            DungeonMap map = game.World;
            Player player = game.Player;

            game.MessageLog.Add($"{player.Name} performs a whirlwind attack against all adjacent enemies");

            List<Point> monsterLocations = new List<Point>();

            foreach (Cell cell in map.GetBorderCellsInSquare(player.X, player.Y, 1))
            {
                foreach (Point monsterLocation in map.GetMonsterLocations())
                {
                    if (cell.X == monsterLocation.X && cell.Y == monsterLocation.Y)
                    {
                        monsterLocations.Add(monsterLocation);
                    }
                }
            }

            foreach (Point monsterLocation in monsterLocations)
            {
                Monster monster = map.GetMonsterAt(monsterLocation.X, monsterLocation.Y);

                if (monster != null)
                {
                    Command.Attack(player, monster, true);
                }
            }

            return true;
        }
    }
}
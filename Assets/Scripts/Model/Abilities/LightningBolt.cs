using RogueSharp;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class LightningBolt : Ability, ITargetable
    {
        private readonly int attack;
        private readonly int attackChance;

        public LightningBolt(Game game, int attack, int attackChance) : base (game)
        {
            Name                = "Lightning Bolt";
            TurnsToRefresh      = 40;
            TurnsUntilRefreshed = 0;
            this.attack         = attack;
            this.attackChance   = attackChance;
        }

        protected override bool PerformAbility()
        {
            return false;
            //return game.TargetingSystem.SelectLine(this);
        }

        public void SelectTarget(Point target)
        {
            DungeonMap map = game.World;
            Player player = game.Player;
            game.MessageLog.Add($"{player.Name} casts a {Name}");

            Actor lightningBoltActor = new Actor(game)
            {
                Attack      = attack,
                AttackChance= attackChance,
                Name        = Name
            };

            foreach (Cell cell in map.GetCellsAlongLine(player.X, player.Y, target.X, target.Y))
            {
                if (cell.IsWalkable)
                {
                    continue;
                }

                if (cell.X == player.X && cell.Y == player.Y)
                {
                    continue;
                }

                Monster monster = map.GetMonsterAt(cell.X, cell.Y);
                if (monster != null)
                {
                    game.commandSystem.Attack(lightningBoltActor, monster);
                }
                else
                {
                    // We hit a wall so stop the bolt
                    // TODO: consider having bolts and fireballs destroy walls and leave rubble
                    return;
                }
            }
        }
    }
}
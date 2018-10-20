using RogueSharp;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class MagicMissile : Ability, ITargetable
    {
        private readonly int attack;
        private readonly int attackChance;

        public MagicMissile(Game game, Actor parent, int attack, int attackChance) : base(game, parent)
        {
            Name                = "Magic Missile";
            TurnsToRefresh      = 10;
            TurnsUntilRefreshed = 0;
            this.attack         = attack;
            this.attackChance   = attackChance;
        }

        protected override bool PerformAbility()
        {
            return game.TargetingSystem.SelectMonster(this);
        }

        public void SelectTarget(Point target)
        {
            DungeonMap map = game.World;
            Player player = game.Player;
            Monster monster = map.GetMonsterAt(target.X, target.Y);

            if (monster != null)
            {
                game.MessageLog.Add($"{player.Name} casts a {Name} at {monster.Name}");
                Actor magicMissleActor = new Actor(game)
                {
                    AttackBase      = attack,
                    AttackChanceBase= attackChance,
                    Name        = Name
                };
                Command.Attack(magicMissleActor, monster, true);
            }
        }
    }
}
using System.Collections.Generic;
using RogueSharp;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class SpearLaunch : Item, ITargetable
    {
        private int Attack { get; set; }
        private int AttackChance { get; set; }

        public SpearLaunch(Game game) : base(game)
        {
            Name = "Throwing Spears";
            RemainingUses = 5;
        }

        protected override bool UseItem()
        {
            return game.TargetingSystem.SelectMonster(this);
        }

        public void SelectTarget(Point target)
        {
            DungeonMap map = game.World;
            Player player = game.Player;
            Actor actorTarget = null;

            if (Owner is Monster)
            {
                actorTarget = map.GetPlayerAt(target.X, target.Y);
            }
            else if (Owner is Player)
            {
                actorTarget = map.GetMonsterAt(target.X, target.Y);
            }

            if (actorTarget != null)
            {
                game.MessageLog.Add($"{Owner.Name} throws a spear at {actorTarget.Name}.");
                Actor throwingSpearActor = new Actor(game)
                {
                    Attack = Attack,
                    AttackChance = AttackChance,
                    Name = Name
                };
                Command.Attack(throwingSpearActor, actorTarget);
            }
        }

    }
}

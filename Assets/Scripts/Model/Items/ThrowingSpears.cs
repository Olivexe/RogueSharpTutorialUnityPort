using System.Collections.Generic;
using RogueSharp;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class ThrowingSpears : Item, ITargetable
    {
        private int Attack { get; set; }
        private int AttackChance { get; set; }

        public ThrowingSpears(Game game) : base(game)
        {
            Name = "Throwing Spears";
            RemainingUses = 5;

            Attack = 5;
            AttackChance = 80;
        }

        public ThrowingSpears(Game game, Actor parent) : base(game)
        {
            Name = "Throwing Spears";
            RemainingUses = 5;
            Owner = parent;
        }

        protected override bool UseItem()
        {
            if (Owner is Player)
            {
                return game.TargetingSystem.SelectMonster(this);
            }
            else
            {
                SelectTarget(new Point(game.Player.X, game.Player.Y));
                return true;
            }
        }

        public void SelectTarget(Point target)
        {
            DungeonMap map      = game.World;
            Player player       = game.Player;
            Actor actorTarget   = null;

            
            if (Owner is Player)
            {
                actorTarget = map.GetMonsterAt(target.X, target.Y);
            }
            else
            {
                actorTarget = map.GetPlayerAt(target.X, target.Y);
            }

            if (actorTarget != null)
            {
                game.MessageLog.Add($"{Owner.Name} throws a spear at {actorTarget.Name}.");
                Actor throwingSpearActor = new Actor(game)
                {
                    AttackBase = Attack,
                    AttackChanceBase = AttackChance,
                    Name = Name
                };

                RemainingUses--;
                Owner.RemoveItemsWithNoRemainingUses();

                Command.Attack(throwingSpearActor, actorTarget, false);
            }
        }
    }
}

using RogueSharp;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Utilities;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class ShootFungalSpike : IBehavior
    {
        public Actor       Parent   { get; private set; }
        public Game        Game     { get; private set; }
        public DungeonMap  World    { get; private set; }

        private int Attack          { get; set; }
        private int AttackChance    { get; set; }

        public void SetBehavior(Game game, Actor parent)
        {
            Game    = game;
            Parent  = parent;
            World   = game.World;
            Attack      = 4;
            AttackChance= 90;
        }

        public bool Act()
        {
            SelectTarget(new Point(Game.Player.X, Game.Player.Y));
            return true;
        }

        public void SelectTarget(Point target)
        {
            Actor actorTarget = null;

            actorTarget = World.GetPlayerAt(target.X, target.Y);

            if (actorTarget != null)
            {
                Game.MessageLog.Add($"{Parent.Name} fires a poisonous spike towards {actorTarget.Name}.");

                Actor PoisonSpike = new Actor(Game)
                {
                    Attack = Attack,
                    AttackChance = AttackChance,
                    Name = "Poisonous Spike"
                };
                
                if(Command.Attack(PoisonSpike, actorTarget))
                {
                    if (Game.Random.Next(1, 10) == 1)
                    {
                        if (actorTarget.AddEffect(new PoisonEffect(Game, actorTarget, 20, 1, 4)))
                        {
                            Game.MessageLog.Add($"You have been poisoned.");
                        }
                    }
                }
            }
        }

    }
}
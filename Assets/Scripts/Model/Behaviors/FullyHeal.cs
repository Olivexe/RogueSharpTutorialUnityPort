using RogueSharpTutorial.Model.Interfaces;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class FullyHeal : IBehavior
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
            if (Parent.Health < Parent.MaxHealth)
            {
                int healthToRecover = Parent.MaxHealth - Parent.Health;
                Parent.Health = Parent.MaxHealth;
                Game.MessageLog.Add($"{Parent.Name} catches their breath and recovers {healthToRecover} health.");
                return true;
            }
            return false;
        }
    }
}

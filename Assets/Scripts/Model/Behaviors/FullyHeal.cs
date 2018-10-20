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
            if (Parent.CurrentHealth < Parent.MaxHealthAdjusted)
            {
                int healthToRecover = Parent.MaxHealthAdjusted - Parent.CurrentHealth;
                Parent.CurrentHealth = Parent.MaxHealthAdjusted;
                Game.MessageLog.Add($"{Parent.Name} catches their breath and recovers {healthToRecover} health.");
                return true;
            }
            return false;
        }
    }
}

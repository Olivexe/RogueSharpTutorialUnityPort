using RogueSharpTutorial.Model.Interfaces;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class FullyHeal : IBehavior
    {
        public bool Act(Actor actor, Game game)
        {
            if (actor.Health < actor.MaxHealth)
            {
                int healthToRecover = actor.MaxHealth - actor.Health;
                actor.Health = actor.MaxHealth;
                game.MessageLog.Add($"{actor.Name} catches his breath and recovers {healthToRecover} health");
                return true;
            }
            return false;
        }
    }
}

using RogueSharpTutorial.Model.Interfaces;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class FullyHeal : IBehavior
    {
        public bool Act(Monster monster, CommandSystem commandSystem, Game game)
        {
            if (monster.Health < monster.MaxHealth)
            {
                int healthToRecover = monster.MaxHealth - monster.Health;
                monster.Health = monster.MaxHealth;
                game.MessageLog.Add($"{monster.Name} catches his breath and recovers {healthToRecover} health");
                return true;
            }
            return false;
        }
    }
}

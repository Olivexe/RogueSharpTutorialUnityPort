using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class DoNothing : Ability
    {
        public DoNothing(Game game) : base (game)
        {
            Name = "None";
            TurnsToRefresh = 0;
            TurnsUntilRefreshed = 0;
        }

        protected override bool PerformAbility()
        {
            game.MessageLog.Add("No ability in that slot");
            return false;
        }
    }
}
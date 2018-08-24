using System;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class Heal : Ability
    {
        private readonly int amountToHeal;

        public Heal(Game game, Actor parent, int amountToHeal) : base (game, parent)
        {
            Name = "Heal Self";
            TurnsToRefresh = 60;
            TurnsUntilRefreshed = 0;
            this.amountToHeal = amountToHeal;
        }

        protected override bool PerformAbility()
        {
            Player player = game.Player;

            player.Health = Math.Min(player.MaxHealth, player.Health + amountToHeal);

            return true;
        }
    }
}

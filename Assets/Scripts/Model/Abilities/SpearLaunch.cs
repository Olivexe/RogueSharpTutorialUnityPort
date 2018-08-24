using System.Collections.Generic;
using RogueSharp;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class SpearLaunch : Ability
    {
        public SpearLaunch(Game game) : base(game)
        {
            Name = "Spear Launch";
            TurnsToRefresh = 20;
            TurnsUntilRefreshed = 0;
        }

        protected override bool PerformAbility()
        {
            return false;
        }
    }
}

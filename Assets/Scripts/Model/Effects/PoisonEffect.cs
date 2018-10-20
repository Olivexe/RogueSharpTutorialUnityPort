using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class PoisonEffect : Effect
    {
        private int damage;
        private int effectInterval;
        private int counter = 0;

        public PoisonEffect(Game game, Actor parent, int lengthOfEffect, int damage, int effectInterval) : base (game, parent)
        {
            Name = "Poisoned";
            Duration = lengthOfEffect;
            this.damage = damage;
            this.effectInterval = effectInterval;
            counter = effectInterval;
        }

        public override bool Perform()
        {
            counter++;

            if(counter >= effectInterval)
            {
                Owner.CurrentHealth -= damage;
                game.MessageLog.Add("The poison weakens you.");
                counter = 0;
            }

            return true;
        }
    }
}
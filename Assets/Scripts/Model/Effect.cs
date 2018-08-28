using RogueSharp;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class Effect : IEffect
    {   
        public string   Name        { get; protected set; }
        public int      Duration    { get; set; }
        public Actor    Owner       { get; set; }

        protected Game  game;

        public Effect(Game game, Actor effectOwner)
        {
            this.game = game;
            Owner = effectOwner;
        }

        public virtual bool Perform()
        {
            return false;
        }

        public void Tick()
        {
            if (Duration > 0)
            {
                Duration--;
            }

            if (Duration == 0)
            {
                game.MessageLog.Add($"{Name} no longer has an effect.");
            }
        }
    }
}

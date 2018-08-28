using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class NoEffect : Effect
    {
        public NoEffect(Game game, Actor parent) : base (game, parent)
        {
            Name = "None";
            Duration = -1;
        }

        public override bool Perform()
        {
            game.MessageLog.Add("No effect to execute.");
            return false;
        }
    }
}
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class NoItem : Item
    {
        public NoItem(Game game) : base(game)
        {
            Name = "None";
            RemainingUses = 1;
        }

        protected override bool UseItem()
        {
            return false;
        }
    }
}

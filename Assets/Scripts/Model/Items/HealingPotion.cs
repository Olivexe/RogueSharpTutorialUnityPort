using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class HealingPotion : Item
    {
        public HealingPotion(Game game) : base(game)
        {
            Name = "Healing Potion";
            RemainingUses = 1;
        }

        protected override bool UseItem()
        {
            int healAmount = 15;
            game.MessageLog.Add($"{game.Player.Name} consumes a {Name} and recovers {healAmount} health");

            Heal heal = new Heal(game, healAmount);

            RemainingUses--;

            return heal.Perform();
        }
    }
}

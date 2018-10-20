using RogueSharp.DiceNotation;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class Whetstone : Item
    {
        public Whetstone(Game game) : base(game)
        {
            Name = "Whetstone";
            RemainingUses = 5;
        }

        protected override bool UseItem()
        {
            Player player = game.Player;

            if (player.MainHand == MainHandEquipment.None(game))
            {
                game.MessageLog.Add($"{player.Name} is not equipping anything they can sharpen");
            }
            else if (player.AttackChanceMeleeAdjusted >= 80)
            {
                game.MessageLog.Add($"{player.Name} cannot make their {player.MainHand.Name} any sharper");
            }
            else
            {
                game.MessageLog.Add($"{player.Name} uses a {Name} to sharpen their {player.MainHand.Name}");
                player.MainHand.AttackChance += Dice.Roll("1D3");
                RemainingUses--;
            }

            return true;
        }
    }
}


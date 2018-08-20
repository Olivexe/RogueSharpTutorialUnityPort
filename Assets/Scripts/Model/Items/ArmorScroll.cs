using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class ArmorScroll : Item
    {
        public ArmorScroll(Game game) : base(game)
        {
            Name = "Armor Scroll";
            RemainingUses = 1;
        }

        protected override bool UseItem()
        {
            Player player = game.Player;

            if (player.Body == BodyEquipment.None(game))
            {
                game.MessageLog.Add($"{player.Name} is not wearing any body armor to enhance");
            }
            else if (player.Defense >= 8)
            {
                game.MessageLog.Add($"{player.Name} cannot enhance their {player.Body.Name} any more");
            }
            else
            {
                game.MessageLog.Add($"{player.Name} uses a {Name} to enhance their {player.Body.Name}");
                player.Body.Defense += 1;
                RemainingUses--;
            }

            return true;
        }
    }
}


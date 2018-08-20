using RogueSharp;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class TeleportScroll : Item
    {
        public TeleportScroll(Game game) : base(game)
        {
            Name = "Teleport Scroll";
            RemainingUses = 1;
        }

        protected override bool UseItem()
        {
            DungeonMap map = game.World;
            Player player = game.Player;

            game.MessageLog.Add($"{player.Name} uses a {Name} and reappears in another place");

            Point point = map.GetRandomLocation();

            map.SetActorPosition(player, point.X, point.Y);

            RemainingUses--;

            return true;
        }
    }
}


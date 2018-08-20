using RogueSharpTutorial.Model;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Utilities
{
    public static class ItemGenerator
    {
        public static Item CreateItem(Game game)
        {
            GamePool<Item> itemPool = new GamePool<Item>();

            itemPool.Add(new ArmorScroll(game), 10);
            itemPool.Add(new DestructionWand(game), 5);
            itemPool.Add(new HealingPotion(game), 20);
            itemPool.Add(new RevealMapScroll(game), 25);
            itemPool.Add(new TeleportScroll(game), 20);
            itemPool.Add(new Whetstone(game), 10);

            return itemPool.Get();
        }
    }
}

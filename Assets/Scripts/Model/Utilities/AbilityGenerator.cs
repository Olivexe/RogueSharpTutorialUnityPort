using RogueSharpTutorial.Model;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Utilities
{
    public static class AbilityGenerator
    {
        public static GamePool<Ability> abilityPool = null;

        public static Ability CreateAbility(Game game)
        {
            if (abilityPool == null)
            {
                abilityPool = new GamePool<Ability>();

                abilityPool.Add(new Heal(game, 10), 10);
                abilityPool.Add(new MagicMissile(game, 2, 80), 10);
                abilityPool.Add(new RevealMap(game, 15), 10);
                abilityPool.Add(new Whirlwind(game), 10);
                abilityPool.Add(new Fireball(game, 4, 60, 2), 10);
                abilityPool.Add(new LightningBolt(game, 6, 40), 10);
            }

            return abilityPool.Get();
        }
    }
}

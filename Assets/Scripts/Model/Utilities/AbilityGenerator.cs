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

                abilityPool.Add(new Heal(game, null, 10), 10);
                abilityPool.Add(new MagicMissile(game, null, 2, 80), 10);
                abilityPool.Add(new RevealMap(game, null, 20), 10);
                abilityPool.Add(new Whirlwind(game, null), 10);
                abilityPool.Add(new Fireball(game, null, 4, 60, 2), 10);
                abilityPool.Add(new LightningBolt(game, null, 6, 40), 10);
            }

            return abilityPool.Get();
        }
    }
}

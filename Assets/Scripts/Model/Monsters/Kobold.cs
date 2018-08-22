using RogueSharp.DiceNotation;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class Kobold : Monster
    {
        public Kobold(Game game) : base(game) { }

        public static Kobold Create(Game game, int level)
        {
            int health = Dice.Roll((level / 3 + 1).ToString() + "D5");

            return new Kobold (game)
            {
                Attack          = Dice.Roll("1D3") + (level / 2),
                AttackChance    = Dice.Roll("25D3") + (level / 2),
                Awareness       = 10,
                Color           = Colors.KoboldColor,
                Defense         = Dice.Roll("1D3") + (level / 2),
                DefenseChance   = Dice.Roll("10D4") + (level / 2),
                Gold            = Dice.Roll("5D5") + (level * 2),
                Health          = health,
                MaxHealth       = health,
                Name            = "Kobold",
                Speed           = 14,
                Symbol          = 'k'
            };
        }
    }
}
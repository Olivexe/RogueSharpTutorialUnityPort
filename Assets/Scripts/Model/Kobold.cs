using RogueSharp;
using RogueSharp.DiceNotation;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.View;

namespace RogueSharpTutorial.Model
{
    public class Kobold : Monster
    {
        public Kobold(Game game) : base(game) { }

        public static Kobold Create(int level, Game game)
        {
            int health = Dice.Roll("2D5");

            return new Kobold (game)
            {
                Attack          = Dice.Roll("1D3") + level / 3,
                AttackChance    = Dice.Roll("25D3"),
                Awareness       = 10,
                Color           = Colors.KoboldColor,
                Defense         = Dice.Roll("1D3") + level / 3,
                DefenseChance   = Dice.Roll("10D4"),
                Gold            = Dice.Roll("5D5"),
                Health          = health,
                MaxHealth       = health,
                Name            = "Kobold",
                Speed           = 14,
                Symbol          = 'k'
            };
        }
    }
}
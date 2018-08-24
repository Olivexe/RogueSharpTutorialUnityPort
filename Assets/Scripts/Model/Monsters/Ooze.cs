using RogueSharp.DiceNotation;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class Ooze : Monster
    {
        public Ooze(Game game) : base(game) { }

        public static Ooze Create(Game game, int level)
        {
            int health = Dice.Roll("4D5") + (level * 2);

            return new Ooze (game)
            {
                Attack          = Dice.Roll("1D2") + (level / 2),
                AttackChance    = Dice.Roll("10D5") + (level / 2),
                Awareness       = 10,
                Color           = Colors.OozeColor,
                Defense         = Dice.Roll("1D2") + (level / 2),
                DefenseChance   = Dice.Roll("10D4"),
                Gold            = Dice.Roll("1D20") + (level * 2),
                Health          = health,
                MaxHealth       = health,
                Name            = "Ooze",
                Speed           = 14,
                Symbol          = 'o'
            };
        }

        public override bool PerformAction(InputCommands command)
        {
            var splitOozeBehavior = new SplitOoze();

            if (!splitOozeBehavior.Act(this, game))
            {
                base.PerformAction(command);
            }

            return true;
        }
    }
}

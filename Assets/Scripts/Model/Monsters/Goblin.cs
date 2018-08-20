using RogueSharp.DiceNotation;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class Goblin : Monster
    {
        private int? turnsSpentRunning  = null;
        private bool shoutedForHelp     = false;

        public Goblin(Game game) : base(game) { }

        public static Goblin Create(Game game, int level)
        {
            int health = Dice.Roll("1D5");

            return new Goblin (game)
            {
                Attack          = Dice.Roll("1D2") + level / 3,
                AttackChance    = Dice.Roll("10D5"),
                Awareness       = 10,
                Color           = Colors.GoblinColor,
                Defense         = Dice.Roll("1D2") + level / 3,
                DefenseChance   = Dice.Roll("10D4"),
                Gold            = Dice.Roll("1D20"),
                Health          = health,
                MaxHealth       = health,
                Name            = "Goblin",
                Speed           = 12,
                Symbol          = 'g'
            };
        }

        public override void PerformAction(CommandSystem commandSystem)
        {
            var fullyHealBehavior       = new FullyHeal();
            var runAwayBehavior         = new RunAway();
            var shoutForHelpBehavior    = new ShoutForHelp();

            if (turnsSpentRunning.HasValue && turnsSpentRunning.Value > 15)
            {
                fullyHealBehavior.Act(this, commandSystem, game);
                turnsSpentRunning = null;
            }
            else if (Health < MaxHealth)
            {
                runAwayBehavior.Act(this, commandSystem, game);

                if (turnsSpentRunning.HasValue)
                {
                    turnsSpentRunning += 1;
                }
                else
                {
                    turnsSpentRunning = 1;
                }

                if (!shoutedForHelp)
                {
                    shoutedForHelp = shoutForHelpBehavior.Act(this, commandSystem, game);
                }
            }
            else
            {
                base.PerformAction(commandSystem);
            }
        }
    }
}
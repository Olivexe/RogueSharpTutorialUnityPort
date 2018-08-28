using RogueSharp.DiceNotation;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class Goblin : Monster
    {
        private int? turnsSpentRunning  = null;
        private bool shoutedForHelp     = false;

        // Behaviors
        private FullyHeal               fullyHeal;
        private RunAway                 runAway;
        private ShoutForHelp            shoutForHelp;
        private StandardMoveAndAttack   standardMoveAndAttack;

        public Goblin(Game game) : base(game) { }

        public static Goblin Create(Game game, int level)
        {
            int health = Dice.Roll((level / 3 + 1).ToString() + "D4");

            return new Goblin (game)
            {
                Attack          = Dice.Roll("1D2") + (level / 3),
                AttackChance    = Dice.Roll("10D5") + (level / 3),
                Awareness       = 6,
                Color           = Colors.GoblinColor,
                Defense         = Dice.Roll("1D2") + (level / 3),
                DefenseChance   = Dice.Roll("10D4") + (level / 3),
                Gold            = Dice.Roll("1D20") + (level * 2),
                Health          = health,
                MaxHealth       = health,
                Name            = "goblin",
                Speed           = 10,
                Symbol          = 'g',
                IsAggressive    = true,
                fullyHeal       = new FullyHeal(),
                runAway         = new RunAway(),
                shoutForHelp    = new ShoutForHelp(),
                standardMoveAndAttack = new StandardMoveAndAttack(),
            };
        }

        public override void SetBehavior()
        {
            fullyHeal.SetBehavior(Game, this);
            runAway.SetBehavior(Game, this);
            shoutForHelp.SetBehavior(Game, this);
            standardMoveAndAttack.SetBehavior(Game, this);
        }

        public override bool PerformAction(InputCommands command)
        {
            FieldOfView.ComputeFov(X, Y, Awareness, true);
            bool isPlayerInView = FieldOfView.IsInFov(Game.Player.X, Game.Player.Y);

            CommonActions.UpdateAlertStatus(this, isPlayerInView);

            if (turnsSpentRunning.HasValue && turnsSpentRunning.Value > 15)
            {
                fullyHeal.Act();
                turnsSpentRunning = null;
            }
            else if (Health < (MaxHealth * .67))
            {
                runAway.Act();

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
                    shoutedForHelp = shoutForHelp.Act();
                }
            }
            else if (TurnsAlerted.HasValue)
            {
                standardMoveAndAttack.Act();
            }

            return true;
        }
    }
}
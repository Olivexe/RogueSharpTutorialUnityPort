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
                AttackBase      = Dice.Roll("1D2") + (level / 3),
                AttackChanceBase= Dice.Roll("10D5") + (level / 3),
                AwarenessBase   = 6,
                Color           = Colors.GoblinColor,
                DefenseBase     = Dice.Roll("1D2") + (level / 3),
                DefenseChanceBase = Dice.Roll("10D4") + (level / 3),
                Gold            = Dice.Roll("1D20") + (level * 2),
                CurrentHealth   = health,
                MaxHealthBase   = health,
                CanGrabTreasure = true,
                Name            = "goblin",
                SpeedBase       = 10,
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
            FieldOfView.ComputeFov(X, Y, AwarenessAdjusted, true);
            bool isPlayerInView = FieldOfView.IsInFov(Game.Player.X, Game.Player.Y);

            CommonActions.UpdateAlertStatus(this, isPlayerInView);

            if (turnsSpentRunning.HasValue && turnsSpentRunning.Value > 15)
            {
                fullyHeal.Act();
                turnsSpentRunning = null;
                runAway.Reset();
            }
            else if (CurrentHealth < (MaxHealthAdjusted * .75))
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
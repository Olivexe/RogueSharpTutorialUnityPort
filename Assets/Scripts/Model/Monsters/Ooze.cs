using RogueSharp.DiceNotation;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class Ooze : Monster
    {
        private SplitOoze               splitOoze;
        private StandardMoveAndAttack   standardMoveAndAttack;

        public Ooze(Game game) : base(game) { }

        public static Ooze Create(Game game, int level)
        {
            int health = Dice.Roll("4D5") + (level * 2);

            return new Ooze (game)
            {
                AttackBase      = Dice.Roll("1D2") + (level / 2),
                AttackChanceBase = Dice.Roll("10D5") + (level / 2),
                AwarenessBase   = 3,
                Color           = Colors.OozeColor,
                DefenseBase     = Dice.Roll("1D2") + (level / 2),
                DefenseChanceBase = Dice.Roll("10D4"),
                Gold            = Dice.Roll("1D20") + (level * 2),
                CurrentHealth   = health,
                MaxHealthBase   = health,
                CanGrabTreasure = false,
                Name            = "ooze",
                SpeedBase       = 16,
                Symbol          = 'o',
                IsAggressive    = true,
                splitOoze       = new SplitOoze(),
                standardMoveAndAttack = new StandardMoveAndAttack()
            };
        }

        public override void SetBehavior()
        {
            splitOoze.SetBehavior(Game, this);
            standardMoveAndAttack.SetBehavior(Game, this);
        }

        public override bool PerformAction(InputCommands command)
        {
            FieldOfView.ComputeFov(X, Y, AwarenessAdjusted, true);
            bool isPlayerInView = FieldOfView.IsInFov(Game.Player.X, Game.Player.Y);

            CommonActions.UpdateAlertStatus(this, isPlayerInView);

            if (!splitOoze.Act())
            {
                if (TurnsAlerted.HasValue)
                {
                    standardMoveAndAttack.Act();
                }
            }

            return true;
        }
    }
}

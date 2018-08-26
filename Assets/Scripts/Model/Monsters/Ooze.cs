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
                Attack          = Dice.Roll("1D2") + (level / 2),
                AttackChance    = Dice.Roll("10D5") + (level / 2),
                Awareness       = 10,
                Color           = Colors.OozeColor,
                Defense         = Dice.Roll("1D2") + (level / 2),
                DefenseChance   = Dice.Roll("10D4"),
                Gold            = Dice.Roll("1D20") + (level * 2),
                Health          = health,
                MaxHealth       = health,
                Name            = "ooze",
                Speed           = 14,
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
            FieldOfView.ComputeFov(X, Y, Awareness, true);
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

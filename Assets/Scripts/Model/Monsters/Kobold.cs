using RogueSharp.DiceNotation;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class Kobold : Monster
    {
        private StandardMoveAndAttack standardMoveAndAttack;

        public Kobold(Game game) : base(game) { }

        public static Kobold Create(Game game, int level)
        {
            int health = Dice.Roll((level / 3 + 1).ToString() + "D5");

            return new Kobold (game)
            {
                Attack          = Dice.Roll("1D3") + (level / 2),
                AttackChance    = Dice.Roll("25D3") + (level / 2),
                Awareness       = 7,
                Color           = Colors.KoboldColor,
                Defense         = Dice.Roll("1D3") + (level / 2),
                DefenseChance   = Dice.Roll("10D4") + (level / 2),
                Gold            = Dice.Roll("5D5") + (level * 2),
                Health          = health,
                MaxHealth       = health,
                Name            = "kobold",
                Speed           = 14,
                Symbol          = 'k',
                IsAggressive    = true,
                standardMoveAndAttack = new StandardMoveAndAttack()

            };
        }

        public override void SetBehavior()
        {
            standardMoveAndAttack.SetBehavior(Game, this);
        }

        public override bool PerformAction(InputCommands command)
        {
            FieldOfView.ComputeFov(X, Y, Awareness, true);
            bool isPlayerInView = FieldOfView.IsInFov(Game.Player.X, Game.Player.Y);

            CommonActions.UpdateAlertStatus(this, isPlayerInView);

            if (TurnsAlerted.HasValue)
            {
                standardMoveAndAttack.Act();
            }

            return true;
        }
    }
}
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
                AttackBase      = Dice.Roll("1D3") + (level / 2),
                AttackChanceBase = Dice.Roll("25D3") + (level / 2),
                AwarenessBase   = 7,
                Color           = Colors.KoboldColor,
                DefenseBase     = Dice.Roll("1D3") + (level / 2),
                DefenseChanceBase = Dice.Roll("10D4") + (level / 2),
                Gold            = Dice.Roll("5D5") + (level * 2),
                CurrentHealth   = health,
                MaxHealthBase   = health,
                CanGrabTreasure = true,
                Name            = "kobold",
                SpeedBase       = 8,
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
            FieldOfView.ComputeFov(X, Y, AwarenessAdjusted, true);
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
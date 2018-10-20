using RogueSharp.DiceNotation;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class FungalSpore : Monster
    {
        private StandardMoveAndAttack standardMoveAndAttack;

        public FungalSpore(Game game) : base(game) { }

        public static FungalSpore Create(Game game, int level)
        {
            int health = Dice.Roll((level / 3 + 1).ToString() + "D3");

            return new FungalSpore(game)
            {
                AttackBase      = Dice.Roll("1D2"),
                AttackChanceBase= Dice.Roll("20D3") + (level / 2),
                AwarenessBase   = 7,
                Color           = Colors.GoblinColor,
                DefenseBase     = Dice.Roll("1D2"),
                DefenseChanceBase = Dice.Roll("10D4"),
                Gold            = 0,
                CurrentHealth   = health,
                MaxHealthBase = health,
                CanGrabTreasure = false,
                Name            = "fungal spore",
                SpeedBase       = 6,
                Symbol          = 's',
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
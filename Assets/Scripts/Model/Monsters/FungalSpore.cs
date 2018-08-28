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
                Attack          = Dice.Roll("1D2"),
                AttackChance    = Dice.Roll("20D3") + (level / 2),
                Awareness       = 7,
                Color           = Colors.GoblinColor,
                Defense         = Dice.Roll("1D2"),
                DefenseChance   = Dice.Roll("10D4"),
                Gold            = 0,
                Health          = health,
                MaxHealth       = health,
                Name            = "fungal spore",
                Speed           = 6,
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
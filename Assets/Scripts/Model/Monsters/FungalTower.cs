using RogueSharp.DiceNotation;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class FungalTower : Monster
    {   
        private int         spikeCounter                = 3;
        private int         spikeCounterlimit           = 3;
        private int         sporeCounter                = 0;
        private int         sporeCounterlimit           = 6;
        private bool        skipSporeSummon             = false;

        // Behaviors 
        private SummonFungalSpore   summonFungalSpore;
        private ShootFungalSpike    shootFungalSpike;

        public FungalTower(Game game) : base(game) { }

        public static FungalTower Create(Game game, int level)
        {
            return new FungalTower(game)
            {
                Attack              = 3,
                AttackChance        = 75,
                Awareness           = 10,
                Color               = Colors.GoblinColor,
                Defense             = 2,
                DefenseChance       = 40,
                Gold                = Dice.Roll("3d10") + 5,
                Health              = 60,
                MaxHealth           = 60,
                CanGrabTreasure     = false,
                Name                = "fungal tower",
                Speed               = 8,
                Symbol              = 'f',
                IsAggressive        = true,
                IsBoss              = false,
                summonFungalSpore   = new SummonFungalSpore(),
                shootFungalSpike    = new ShootFungalSpike()
            };
        }

        public override void SetBehavior()
        {
            summonFungalSpore.SetBehavior(Game, this, MonsterList.fungalSpore, 1);
            shootFungalSpike.SetBehavior(Game, this);
        }

        public override bool PerformAction(InputCommands command)
        {
            FieldOfView.ComputeFov(X, Y, Awareness, true);
            bool isPlayerInView = FieldOfView.IsInFov(Game.Player.X, Game.Player.Y);

            CommonActions.UpdateAlertStatus(this, isPlayerInView);

            if (TurnsAlerted.HasValue)
            {
                if (spikeCounter == spikeCounterlimit)
                {
                    if (isPlayerInView)
                    {
                        shootFungalSpike.Act();
                        spikeCounter = 0;
                        skipSporeSummon = true;
                    }
                }
            }

            if (!skipSporeSummon && sporeCounter >= sporeCounterlimit)
            {
                if (isPlayerInView)
                {
                    summonFungalSpore.Act();
                    sporeCounter = 0;
                }
            }

            if (spikeCounter < spikeCounterlimit)
            {
                spikeCounter++;
            }

            if (sporeCounter < sporeCounterlimit)
            {
                sporeCounter++;
            }

            skipSporeSummon = false;

            return true;
        }
    }
}
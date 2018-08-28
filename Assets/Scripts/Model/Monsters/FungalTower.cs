using RogueSharp.DiceNotation;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class FungalTower : Monster
    {   
        private int         spikeCounter                = 3;
        private int         spikeCounterlimit           = 3;
        private int         sporeCounter                = 20;
        private int         sporeCounterlimit           = 0;
        private bool        skipSporeSummon             = false;

        // Behaviors 
        private SummonMonster       summonFungalTower;
        private SummonMonster       summonFungalSpore;
        private ShootFungalSpike    shootFungalSpike;

        public FungalTower(Game game) : base(game) { }

        public static FungalTower Create(Game game, int level)
        {
            return new FungalTower(game)
            {
                Attack              = 3,
                AttackChance        = 75,
                Awareness           = 8,
                Color               = Colors.GoblinColor,
                Defense             = 2,
                DefenseChance       = 40,
                Gold                = Dice.Roll("3d10") + 5,
                Health              = 60,
                MaxHealth           = 60,
                Name                = "fungal tower",
                Speed               = 8,
                Symbol              = 'f',
                IsAggressive        = true,
                IsBoss              = false,
                summonFungalTower   = new SummonMonster(),
                summonFungalSpore   = new SummonMonster(),
                shootFungalSpike    = new ShootFungalSpike()
            };
        }

        public override void SetBehavior()
        {
            summonFungalTower.SetBehavior(Game, this, MonsterList.fungalTower, 1);
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

            if (!skipSporeSummon && sporeCounter == sporeCounterlimit)
            {
                if (isPlayerInView)
                {
                    if (Game.Random.Next(6) == 0)
                    {
                        summonFungalTower.Act();
                    }
                    else
                    {
                        summonFungalSpore.Act();
                    }

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
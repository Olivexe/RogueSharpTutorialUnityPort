using RogueSharp.DiceNotation;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class Barbarian : Monster
    {
        private int         turnsAfterSpear         = 5;
        private int         turnsBetweenSpearThrows = 5;
        private bool        madeFirstSummon         = false;
        private bool        madeSecondSummon        = false;

        // Behaviors 
        private SummonMonster           summonHelp;
        private StandardMoveAndAttack   standardMoveAndAttack;

        public Barbarian(Game game) : base(game) { }

        public static Barbarian Create(Game game, int level)
        {
            return new Barbarian(game)
            {
                AttackBase          = 3,
                AttackChanceBase    = 75,
                AwarenessBase       = 8,
                Color               = Colors.GoblinColor,
                DefenseBase         = 3,
                DefenseChanceBase   = 65,
                Gold                = Dice.Roll("5D20") + 50,
                CurrentHealth       = 40,
                MaxHealthBase       = 40,
                CanGrabTreasure     = true,
                Name                = "Barbarian",
                SpeedBase           = 8,
                Symbol              = 'B',
                IsAggressive        = true,
                IsBoss              = true,
                summonHelp          = new SummonMonster(),
                standardMoveAndAttack = new StandardMoveAndAttack(),
                Item1               = new ThrowingSpears(game)
            };
        }

        public override void SetBehavior()
        {
            summonHelp.SetBehavior(Game, this, MonsterList.goblin, 3);
            standardMoveAndAttack.SetBehavior(Game, this);
        }

        public override bool PerformAction(InputCommands command)
        {
            FieldOfView.ComputeFov(X, Y, AwarenessBase, true);
            bool isPlayerInView = FieldOfView.IsInFov(Game.Player.X, Game.Player.Y);

            CommonActions.UpdateAlertStatus(this, isPlayerInView);

            if (TurnsAlerted.HasValue)
            {
                if (HaveAnySpears() && turnsAfterSpear == turnsBetweenSpearThrows)
                {
                    if (isPlayerInView)
                    {
                        UseSpearItem();
                        turnsAfterSpear = 1;
                    }
                }
                else if(CurrentHealth < (MaxHealthAdjusted * .5) && !madeFirstSummon)
                {
                    summonHelp.Act();
                    madeFirstSummon = true;
                }
                else if (CurrentHealth < (MaxHealthAdjusted * .25) && !madeSecondSummon)
                {
                    summonHelp.Act();
                    madeSecondSummon = true;
                }
                else
                {
                    standardMoveAndAttack.Act();
                }
            }
            else
            {
                //Do nothing, esentially 'sleep' till player in view
            }

            if(HaveAnySpears() && turnsAfterSpear < turnsBetweenSpearThrows)
            {
                turnsAfterSpear++;
            }
          
            return true;
        }

        private bool HaveAnySpears()
        {
            if(Item1 is ThrowingSpears || Item2 is ThrowingSpears || Item3 is ThrowingSpears || Item4 is ThrowingSpears)
            {
                return true;
            }
            return false;
        }

        private void UseSpearItem()
        {
            if      (Item1 is ThrowingSpears)
                UseItem(1);
            else if (Item2 is ThrowingSpears)
                UseItem(2);
            else if (Item3 is ThrowingSpears)
                UseItem(3);
            else if (Item4 is ThrowingSpears)
                UseItem(4);
        }
    }
}
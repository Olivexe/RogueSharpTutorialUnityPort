using RogueSharpTutorial.View;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class Player : Actor
    { 
        public int      MonsterScore{ get; set; }
        public int      LevelScore  { get { return Game.mapLevel * 2; } }
        public int      GoldScore   { get { return Gold / 5; } }
        public int      TotalScore  { get { return MonsterScore + GoldScore + LevelScore; } }

        public Player(Game game) : base(game){ }

        public override bool PerformAction(InputCommands command)
        {
            switch (command)
            {
                case InputCommands.UpLeft:
                    return Command.Move(this, X - 1, Y + 1);
                case InputCommands.Up:
                    return Command.Move(this, X    , Y + 1);
                case InputCommands.UpRight:
                    return Command.Move(this, X + 1, Y + 1);
                case InputCommands.Left:
                    return Command.Move(this, X - 1, Y    );
                case InputCommands.Right:
                    return Command.Move(this, X + 1, Y    );
                case InputCommands.DownLeft:
                    return Command.Move(this, X - 1, Y - 1);
                case InputCommands.Down:
                    return Command.Move(this, X    , Y - 1);
                case InputCommands.DownRight:
                    return Command.Move(this, X + 1, Y - 1);
                case InputCommands.QAbility:
                    return QAbility.Perform();
                case InputCommands.WAbility:
                    return WAbility.Perform();
                case InputCommands.EAbility:
                    return EAbility.Perform();
                case InputCommands.RAbility:
                    return RAbility.Perform();
                case InputCommands.Item1:
                    return UseItem(1);
                case InputCommands.Item2:
                    return UseItem(2);
                case InputCommands.Item3:
                    return UseItem(3);
                case InputCommands.Item4:
                    return UseItem(4);
                case InputCommands.ForgetQAbility:
                    QAbility = new DoNothing(Game, this);
                    return true;
                case InputCommands.ForgetWAbility:
                    WAbility = new DoNothing(Game, this);
                    return true;
                case InputCommands.ForgetEAbility:
                    EAbility = new DoNothing(Game, this);
                    return true;
                case InputCommands.ForgetRAbility:
                    RAbility = new DoNothing(Game, this);
                    return true;
                default:
                    break;
            }

            return false;
        }

        public void DrawStats()
        {
            Game.DrawPlayerStats();
            Game.DrawPlayerInventory();
        }
    }
}
using RogueSharpTutorial.View;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class Player : Actor
    { 
        public int      MonsterScore{ get; set; }
        public int      LevelScore  { get { return game.mapLevel * 2; } }
        public int      GoldScore   { get { return Gold / 5; } }
        public int      TotalScore  { get { return MonsterScore + GoldScore + LevelScore; } }

        public Player(Game game) : base(game)
        {
            QAbility = new DoNothing(game);
            WAbility = new DoNothing(game);
            EAbility = new DoNothing(game);
            RAbility = new DoNothing(game);

            Item1 = new NoItem(game);
            Item2 = new NoItem(game);
            Item3 = new NoItem(game);
            Item4 = new NoItem(game);
        }

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
                    return UseItem(1, game);
                case InputCommands.Item2:
                    return UseItem(2, game);
                case InputCommands.Item3:
                    return UseItem(3, game);
                case InputCommands.Item4:
                    return UseItem(4, game);
                default:
                    break;
            }

            return false;
        }

        public void DrawStats()
        {
            game.DrawPlayerStats();
            game.DrawPlayerInventory();
        }
    }
}
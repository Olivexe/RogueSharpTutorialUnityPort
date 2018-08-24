using RogueSharpTutorial.View;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class Player : Actor
    { 
        public IAbility QAbility    { get; set; }
        public IAbility WAbility    { get; set; }
        public IAbility EAbility    { get; set; }
        public IAbility RAbility    { get; set; }

        public IItem    Item1       { get; set; }
        public IItem    Item2       { get; set; }
        public IItem    Item3       { get; set; }
        public IItem    Item4       { get; set; }

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

        private bool UseItem(int itemNum, Game game)
        {
            bool didUseItem = false;

            if (itemNum == 1)
            {
                didUseItem = Item1.Use();
            }
            else if (itemNum == 2)
            {
                didUseItem = Item2.Use();
            }
            else if (itemNum == 3)
            {
                didUseItem = Item3.Use();
            }
            else if (itemNum == 4)
            {
                didUseItem = Item4.Use();
            }

            if (didUseItem)
            {
                RemoveItemsWithNoRemainingUses(game);
            }

            return didUseItem;
        }

        private void RemoveItemsWithNoRemainingUses(Game game)
        {
            if (Item1.RemainingUses <= 0)
            {
                Item1 = new NoItem(game);
            }
            if (Item2.RemainingUses <= 0)
            {
                Item2 = new NoItem(game);
            }
            if (Item3.RemainingUses <= 0)
            {
                Item3 = new NoItem(game);
            }
            if (Item4.RemainingUses <= 0)
            {
                Item4 = new NoItem(game);
            }
        }

        public bool AddAbility(IAbility ability)
        {
            if (QAbility is DoNothing)
            {
                QAbility = ability;
            }
            else if (WAbility is DoNothing)
            {
                WAbility = ability;
            }
            else if (EAbility is DoNothing)
            {
                EAbility = ability;
            }
            else if (RAbility is DoNothing)
            {
                RAbility = ability;
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool AddItem(IItem item)
        {
            if (Item1 is NoItem)
            {
                Item1 = item;
            }
            else if (Item2 is NoItem)
            {
                Item2 = item;
            }
            else if (Item3 is NoItem)
            {
                Item3 = item;
            }
            else if (Item4 is NoItem)
            {
                Item4 = item;
            }
            else
            {
                return false;
            }

            return true;
        }

        public void Tick()
        {
            QAbility?.Tick();
            WAbility?.Tick();
            EAbility?.Tick();
            RAbility?.Tick();
        }

        public void DrawStats()
        {
            game.DrawPlayerStats();
            game.DrawPlayerInventory();
        }
    }
}
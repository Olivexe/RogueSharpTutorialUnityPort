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
        public int      LevelScore  { get { return game.mapLevel / 3; } }
        public int      GoldScore   { get { return Gold / 4; } }
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
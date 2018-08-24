using RogueSharpTutorial.View;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;
using RogueSharp;

namespace RogueSharpTutorial.Model
{
    public class Actor : IActor, IDrawable, IScheduleable
    {
        // IActor
        public string   Name            { get; set; }
        public int      Gold            { get; set; }
        public int      Health          { get; set; }

        private int     attack;
        public  int     Attack          { get { return attack + Head.Attack + Body.Attack + Hand.Attack + Feet.Attack; } set { attack = value; } }
        private int     attackChance;
        public  int     AttackChance    { get { return attackChance + Head.AttackChance + Body.AttackChance + Hand.AttackChance + Feet.AttackChance; } set { attackChance = value; } }
        private int     defense;
        public  int     Defense         { get { return defense + Head.Defense + Body.Defense + Hand.Defense + Feet.Defense; } set { defense = value; } }
        private int     defenseChance;
        public  int     DefenseChance   { get { return defenseChance + Head.DefenseChance + Body.DefenseChance + Hand.DefenseChance + Feet.DefenseChance; } set { defenseChance = value; } }
        private int     maxHealth;
        public  int     MaxHealth       { get { return maxHealth + Head.MaxHealth + Body.MaxHealth + Hand.MaxHealth + Feet.MaxHealth; } set { maxHealth = value; } }
        private int     speed;
        public  int     Speed           { get { return speed + Head.Speed + Body.Speed + Hand.Speed + Feet.Speed; } set { speed = value; } }
        private int     awareness;
        public  int     Awareness       { get { return awareness + Head.Awareness + Body.Awareness + Hand.Awareness + Feet.Awareness; } set { awareness = value; } }

        public  HeadEquipment   Head        { get; set; }
        public  BodyEquipment   Body        { get; set; }
        public  HandEquipment   Hand        { get; set; }
        public  FeetEquipment   Feet        { get; set; }

        public IAbility         QAbility    { get; set; }
        public IAbility         WAbility    { get; set; }
        public IAbility         EAbility    { get; set; }
        public IAbility         RAbility    { get; set; }

        public IItem            Item1       { get; set; }
        public IItem            Item2       { get; set; }
        public IItem            Item3       { get; set; }
        public IItem            Item4       { get; set; }


        // IDrawable
        public  Colors  Color           { get; set; }
        public  char    Symbol          { get; set; }
        public  int     X               { get; set; }
        public  int     Y               { get; set; }

        // Ischeduleable
        public  int     Time            { get {return Speed;} }

        protected Game  game;

        public Actor(Game game)
        {
            this.game = game;

            Head = HeadEquipment.None(game);
            Body = BodyEquipment.None(game);
            Hand = HandEquipment.None(game);
            Feet = FeetEquipment.None(game);
        }

        public void Draw(IMap map)
        {
            // Only draw the actor with the color and symbol when they are in field-of-view
            if (map.IsInFov(X, Y))
            {
                game.SetMapCell(X, Y, Color, Colors.FloorBackgroundFov, Symbol, map.GetCell(X, Y).IsExplored);
            }
            else
            {
                // When not in field-of-view just draw a normal floor
                game.SetMapCell(X, Y, Colors.Floor, Colors.FloorBackground, '.', map.GetCell(X, Y).IsExplored);
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

        protected bool UseItem(int itemNum, Game game)
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

        public void Tick()
        {
            QAbility?.Tick();
            WAbility?.Tick();
            EAbility?.Tick();
            RAbility?.Tick();
        }

        public virtual bool PerformAction(InputCommands command)
        {
            var     behavior = new StandardMoveAndAttack();
            return  behavior.Act(this, game);
        }
    }
}
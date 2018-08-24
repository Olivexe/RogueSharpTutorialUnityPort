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

        public  HeadEquipment Head      { get; set; }
        public  BodyEquipment Body      { get; set; }
        public  HandEquipment Hand      { get; set; }
        public  FeetEquipment Feet      { get; set; }

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

        public virtual bool PerformAction(InputCommands command)
        {
            var     behavior = new StandardMoveAndAttack();
            return  behavior.Act(this, game);
        }
    }
}
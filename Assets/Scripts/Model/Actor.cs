using RogueSharpTutorial.View;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;
using RogueSharp;

namespace RogueSharpTutorial.Model
{
    public class Actor : IActor, IDrawable, IScheduleable
    {
        // IActor
        private int     attack;
        public  int     Attack          { get { return attack; }        set { attack = value; } }
        private int     attackChance;
        public  int     AttackChance    { get { return attackChance; }  set { attackChance = value; } }
        private int     defense;
        public  int     Defense         { get { return defense; }       set { defense = value; } }
        private int     defenseChance;
        public  int     DefenseChance   { get { return defenseChance; } set { defenseChance = value; } }
        private int     gold;
        public  int     Gold            { get { return gold; }          set { gold = value; } }
        private int     health;
        public  int     Health          { get { return health; }        set { health = value; } }
        private int     maxHealth;
        public  int     MaxHealth       { get { return maxHealth; }     set { maxHealth = value; } }
        private int     speed;
        public  int     Speed           { get { return speed; }         set { speed = value; } }
        private string  name;
        public string   Name            { get { return name; }          set { name = value; } }
        private int     awareness;
        public  int     Awareness       { get { return awareness; }     set { awareness = value; } }

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
    }
}
using RogueSharpTutorial.Controller;
using RogueSharp.DiceNotation;

namespace RogueSharpTutorial.Model
{
    public class Ammunition : Equipment
    {
        public Ammunition(Game gameIn) : base(gameIn) { }

        public static Ammunition None(Game gameIn)
        {
            return new Ammunition(gameIn)
            {
                game = gameIn,
                Name = "None"
            };
        }

        public static Ammunition Arrow(Game gameIn)
        {
            return new Ammunition(gameIn)
            {
                game = gameIn,
                Attack = 1,
                AttackChance = 10,
                Name = "Arrow",
                Speed = 0,
                MaxStack = 100,
                CurrentStackAmount = Dice.Roll("5D6")
            };
        }
    }
}
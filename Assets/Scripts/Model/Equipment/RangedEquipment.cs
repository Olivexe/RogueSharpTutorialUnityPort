using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class RangedEquipment : Equipment
    {
        public RangedEquipment(Game gameIn) : base(gameIn) { }

        public static RangedEquipment None(Game gameIn)
        {
            return new RangedEquipment(gameIn)
            {
                game = gameIn,
                Name = "None"
            };
        }

        public static RangedEquipment Bow(Game gameIn)
        {
            return new RangedEquipment(gameIn)
            {
                game = gameIn,
                Attack = 1,
                AttackChance = 20,
                Name = "Bow",
                Speed = 0
            };
        }
    }
}
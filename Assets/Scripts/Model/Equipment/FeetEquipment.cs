using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class FeetEquipment : Equipment
    {
        public FeetEquipment(Game gameIn) : base(gameIn) { }

        public static FeetEquipment None(Game gameIn)
        {
            return new FeetEquipment(gameIn)
            {
                game = gameIn,
                Name = "None"
            };
        }

        public static FeetEquipment Leather(Game gameIn)
        {
            return new FeetEquipment(gameIn)
            {
                game = gameIn,
                Defense = 1,
                DefenseChance = 5,
                Name = "Leather"
            };
        }

        public static FeetEquipment Chain(Game gameIn)
        {
            return new FeetEquipment(gameIn)
            {
                game = gameIn,
                Defense = 1,
                DefenseChance = 10,
                Name = "Chain"
            };
        }

        public static FeetEquipment Plate(Game gameIn)
        {
            return new FeetEquipment(gameIn)
            {
                game = gameIn,
                Defense = 1,
                DefenseChance = 15,
                Name = "Plate"
            };
        }
    }
}
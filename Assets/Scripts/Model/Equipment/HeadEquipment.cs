using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class HeadEquipment : Equipment
    {
        public HeadEquipment(Game gameIn) : base(gameIn) { }

        public static HeadEquipment None(Game gameIn)
        {
            return new HeadEquipment(gameIn)
            {
                game = gameIn,
                Name = "None"
            };
        }

        public static HeadEquipment Leather(Game gameIn)
        {
            return new HeadEquipment(gameIn)
            {
                game = gameIn,
                Defense = 1,
                DefenseChance = 5,
                Name = "Leather"
            };
        }

        public static HeadEquipment Chain(Game gameIn)
        {
            return new HeadEquipment(gameIn)
            {
                game = gameIn,
                Defense = 1,
                DefenseChance = 10,
                Name = "Chain"
            };
        }

        public static HeadEquipment Plate(Game gameIn)
        {
            return new HeadEquipment(gameIn)
            {
                game = gameIn,
                Defense = 1,
                DefenseChance = 15,
                Name = "Plate"
            };
        }
    }
}
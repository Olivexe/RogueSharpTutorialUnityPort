using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class BodyEquipment : Equipment
    {
        public BodyEquipment(Game gameIn) : base(gameIn) {}

        public static BodyEquipment None(Game gameIn)
        {
            return new BodyEquipment(gameIn)
            {
                game = gameIn,
                Name = "None"
            };
        }

        public static BodyEquipment Leather(Game gameIn)
        {
            return new BodyEquipment(gameIn)
            {
                game = gameIn,
                Defense = 1,
                DefenseChance = 10,
                Name = "Leather Body"
            };
        }

        public static BodyEquipment Chain(Game gameIn)
        {
            return new BodyEquipment(gameIn)
            {
                game = gameIn,
                Defense = 2,
                DefenseChance = 5,
                Name = "Chain Body"
            };
        }

        public static BodyEquipment Plate(Game gameIn)
        {
            return new BodyEquipment(gameIn)
            {
                game = gameIn,
                Defense = 2,
                DefenseChance = 10,
                Name = "Plate Body"
            };
        }
    }
}
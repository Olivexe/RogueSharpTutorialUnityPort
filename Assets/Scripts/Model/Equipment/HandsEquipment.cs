using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class HandsEquipment : Equipment
    {
        public HandsEquipment(Game gameIn) : base(gameIn) { }

        public static HandsEquipment None(Game gameIn)
        {
            return new HandsEquipment(gameIn)
            {
                game = gameIn,
                Name = "None"
            };
        }

        public static HandsEquipment Leather(Game gameIn)
        {
            return new HandsEquipment(gameIn)
            {
                game = gameIn,
                Defense = 1,
                DefenseChance = 5,
                Name = "Leather Gloves"
            };
        }

        public static HandsEquipment Chain(Game gameIn)
        {
            return new HandsEquipment(gameIn)
            {
                game = gameIn,
                Defense = 1,
                DefenseChance = 10,
                Name = "Chain Gloves"
            };
        }

        public static HandsEquipment Plate(Game gameIn)
        {
            return new HandsEquipment(gameIn)
            {
                game = gameIn,
                Defense = 1,
                DefenseChance = 15,
                Name = "Plate Gloves"
            };
        }
    }
}
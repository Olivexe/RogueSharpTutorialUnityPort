using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class HandEquipment : Equipment
    {
        public HandEquipment(Game gameIn) : base(gameIn) { }

        public static HandEquipment None(Game gameIn)
        {
            return new HandEquipment(gameIn)
            {
                game = gameIn,
                Name = "None"
            };
        }

        public static HandEquipment Dagger(Game gameIn)
        {
            return new HandEquipment(gameIn)
            {
                game = gameIn,
                Attack = 1,
                AttackChance = 10,
                Name = "Dagger",
                Speed = -2
            };
        }

        public static HandEquipment Sword(Game gameIn)
        {
            return new HandEquipment(gameIn)
            {
                game = gameIn,
                Attack = 1,
                AttackChance = 20,
                Name = "Sword",
                Speed = 0
            };
        }

        public static HandEquipment Axe(Game gameIn)
        {
            return new HandEquipment(gameIn)
            {
                game = gameIn,
                Attack = 2,
                AttackChance = 15,
                Name = "Axe",
                Speed = 1
            };
        }

        public static HandEquipment TwoHandedSword(Game gameIn)
        {
            return new HandEquipment(gameIn)
            {
                game = gameIn,
                Attack = 3,
                AttackChance = 30,
                Name = "2H Sword",
                Speed = 3
            };
        }
    }
}
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class MainHandEquipment : Equipment
    {
        public MainHandEquipment(Game gameIn) : base(gameIn) { }

        public static MainHandEquipment None(Game gameIn)
        {
            return new MainHandEquipment(gameIn)
            {
                game = gameIn,
                Name = "None"
            };
        }

        public static MainHandEquipment Dagger(Game gameIn)
        {
            return new MainHandEquipment(gameIn)
            {
                game = gameIn,
                Attack = 1,
                AttackChance = 10,
                Name = "Dagger",
                Speed = -2
            };
        }

        public static MainHandEquipment Sword(Game gameIn)
        {
            return new MainHandEquipment(gameIn)
            {
                game = gameIn,
                Attack = 1,
                AttackChance = 20,
                Name = "Sword",
                Speed = 0
            };
        }

        public static MainHandEquipment Axe(Game gameIn)
        {
            return new MainHandEquipment(gameIn)
            {
                game = gameIn,
                Attack = 2,
                AttackChance = 15,
                Name = "Axe",
                Speed = 1
            };
        }

        public static MainHandEquipment TwoHandedSword(Game gameIn)
        {
            return new MainHandEquipment(gameIn)
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
using RogueSharpTutorial.Model;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Utilities
{
    public class EquipmentGenerator
    {
        private readonly GamePool<Equipment> equipmentPool;

        public EquipmentGenerator(Game game, int level)
        {
            equipmentPool = new GamePool<Equipment>();

            if (level <= 3)
            {
                equipmentPool.Add(BodyEquipment.Leather(game), 20);
                equipmentPool.Add(HeadEquipment.Leather(game), 20);
                equipmentPool.Add(FeetEquipment.Leather(game), 20);
                equipmentPool.Add(MainHandEquipment.Dagger(game), 25);
                equipmentPool.Add(MainHandEquipment.Sword(game), 5);
                equipmentPool.Add(HeadEquipment.Chain(game), 5);
                equipmentPool.Add(BodyEquipment.Chain(game), 5);
            }
            else if (level <= 5)
            {
                equipmentPool.Add(BodyEquipment.Chain(game), 20);
                equipmentPool.Add(HeadEquipment.Chain(game), 20);
                equipmentPool.Add(FeetEquipment.Chain(game), 20);
                equipmentPool.Add(MainHandEquipment.Sword(game), 15);
                equipmentPool.Add(MainHandEquipment.Axe(game), 15);
                equipmentPool.Add(HeadEquipment.Plate(game), 5);
                equipmentPool.Add(BodyEquipment.Plate(game), 5);
            }
            else
            {
                equipmentPool.Add(BodyEquipment.Plate(game), 25);
                equipmentPool.Add(HeadEquipment.Plate(game), 25);
                equipmentPool.Add(FeetEquipment.Plate(game), 25);
                equipmentPool.Add(RangedEquipment.Bow(game), 25);
                equipmentPool.Add(Ammunition.Arrow(game), 25);
                equipmentPool.Add(MainHandEquipment.TwoHandedSword(game), 25);
            }
        }

        public Equipment CreateEquipment()
        {
            return equipmentPool.Get();
        }
    }
}

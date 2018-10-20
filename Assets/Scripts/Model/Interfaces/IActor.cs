using System.Collections.Generic;

namespace RogueSharpTutorial.Model.Interfaces
{
    public interface IActor
    {
        HeadEquipment       Head        { get; set; }
        BodyEquipment       Body        { get; set; }
        HandsEquipment      Hands       { get; set; }
        FeetEquipment       Feet        { get; set; }
        MainHandEquipment   MainHand    { get; set; }
        RangedEquipment     Ranged      { get; set; }
        Ammunition          AmmoCarried { get; set; }

        IAbility QAbility       { get; set; }
        IAbility WAbility       { get; set; }
        IAbility EAbility       { get; set; }
        IAbility RAbility       { get; set; }

        IItem Item1             { get; set; }
        IItem Item2             { get; set; }
        IItem Item3             { get; set; }
        IItem Item4             { get; set; }

        List<IEffect> Effects   { get; set; }
        int MaxEffects          { get; set; }

        List<IInventory> Inventory { get; set; }
        int MaxInventory        { get; set; }

        int AttackBase          { get; set; }
        int AttackMeleeAdjusted { get; }
        int AttackRangedAdjusted{ get; }

        int AttackChanceBase    { get; set; }
        int AttackChanceMeleeAdjusted   { get; }
        int AttackChanceRangedAdjusted  { get; }

        int DefenseBase         { get; set; }
        int DefenseAdjusted     { get; }

        int DefenseChanceBase   { get; set; }
        int DefenseChanceAdjusted{ get; }

        int MaxHealthBase       { get; set; }
        int MaxHealthAdjusted   { get; }
        int CurrentHealth       { get; set; }

        int SpeedBase           { get; set; }
        int SpeedAdjusted       { get; }

        int AwarenessBase       { get; set; }
        int AwarenessAdjusted   { get; }
        bool CanGrabTreasure    { get; set; }
    }
}
using System.Collections.Generic;

namespace RogueSharpTutorial.Model.Interfaces
{
    public interface IActor
    {
        HeadEquipment Head  { get; set; }
        BodyEquipment Body  { get; set; }
        HandEquipment Hand  { get; set; }
        FeetEquipment Feet  { get; set; }

        IAbility QAbility   { get; set; }
        IAbility WAbility   { get; set; }
        IAbility EAbility   { get; set; }
        IAbility RAbility   { get; set; }

        IItem Item1         { get; set; }
        IItem Item2         { get; set; }
        IItem Item3         { get; set; }
        IItem Item4         { get; set; }

        List<IEffect> Effects{ get; set; }
        int MaxEffects      { get; set; }

        List<IInventory> Inventory { get; set; }
        int MaxWeight       { get; set; }

        int Attack          { get; set; }
        int AttackChance    { get; set; }
        int Awareness       { get; set; }
        int Defense         { get; set; }
        int DefenseChance   { get; set; }
        int Gold            { get; set; }
        int Health          { get; set; }
        int MaxHealth       { get; set; }
        string Name         { get; set; }
        int Speed           { get; set; }
        bool CanGrabTreasure{ get; set; }
    }
}
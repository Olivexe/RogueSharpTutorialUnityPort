namespace RogueSharpTutorial.Model.Interfaces
{
    public interface IInventory
    {
        int MaxStack            { get; set; }
        int CurrentStackAmount  { get; set; }
    }
}
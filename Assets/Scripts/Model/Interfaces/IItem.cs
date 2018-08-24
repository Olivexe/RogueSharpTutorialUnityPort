namespace RogueSharpTutorial.Model.Interfaces
{
    public interface IItem
    {
        string  Name            { get; }
        int     RemainingUses   { get; }
        Actor   Owner           { get; set; }

        bool Use();
    }
}

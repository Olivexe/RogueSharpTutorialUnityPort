using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model.Interfaces
{
    public interface IBehavior
    {
        Actor       Parent  { get; }
        Game        Game    { get; }
        DungeonMap  World   { get; }

        bool Act();
    }
}
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model.Interfaces
{
    public interface IBehavior
    {
        bool Act(Monster monster, CommandSystem commandSystem, Game game);
    }
}
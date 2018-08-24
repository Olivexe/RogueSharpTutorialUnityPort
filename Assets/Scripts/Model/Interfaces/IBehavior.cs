using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model.Interfaces
{
    public interface IBehavior
    {
        bool Act(Actor monster, Game game);
    }
}
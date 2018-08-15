using RogueSharp;

namespace RogueSharpTutorial.Model.Interfaces
{
    public interface IDrawable
    {
        Colors  Color   { get; set; }
        char    Symbol  { get; set; }
        int     X       { get; set; }
        int     Y       { get; set; }

        void Draw(IMap map);
    }
}
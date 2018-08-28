namespace RogueSharpTutorial.Model.Interfaces
{
    public interface IEffect
    {
        string  Name        { get; }
        int     Duration    { get; set; }
        Actor   Owner       { get; set; }

        void Tick();
    }
}

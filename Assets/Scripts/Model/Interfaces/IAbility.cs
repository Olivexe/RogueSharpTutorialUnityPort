namespace RogueSharpTutorial.Model.Interfaces
{
    public interface IAbility
    {
        string  Name                { get; }
        int     TurnsToRefresh      { get; }
        int     TurnsUntilRefreshed { get; }
        Actor   Owner               { get; set; }

        bool Perform();
        void Tick();
    }
}

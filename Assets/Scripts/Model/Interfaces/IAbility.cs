namespace RogueSharpTutorial.Model.Interfaces
{
    public interface IAbility
    {
        string  Name                { get; }
        int     TurnsToRefresh      { get; }
        int     TurnsUntilRefreshed { get; }
        Actor   Actor               { get; }

        bool Perform();
        void Tick();
    }
}

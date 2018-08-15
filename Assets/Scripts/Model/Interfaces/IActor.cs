namespace RogueSharpTutorial.Model.Interfaces
{
    public interface IActor
    {
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
    }
}
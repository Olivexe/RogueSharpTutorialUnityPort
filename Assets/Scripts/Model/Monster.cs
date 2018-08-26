using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class Monster : Actor
    {
        public int? TurnsAlerted    { get; set; }
        public bool IsAggressive    { get; set; }
        public bool IsBoss          { get; set; }

        public Monster(Game game) : base(game) { }

        public static Monster Clone(Game game, Monster anotherMonster)
        {
            return new Ooze(game)
            {
                Attack          = anotherMonster.Attack,
                AttackChance    = anotherMonster.AttackChance,
                Awareness       = anotherMonster.Awareness,
                Color           = anotherMonster.Color,
                Defense         = anotherMonster.Defense,
                DefenseChance   = anotherMonster.DefenseChance,
                Gold            = 0,
                Health          = anotherMonster.Health,
                MaxHealth       = anotherMonster.MaxHealth,
                Name            = anotherMonster.Name,
                Speed           = anotherMonster.Speed,
                Symbol          = anotherMonster.Symbol,
                IsAggressive    = anotherMonster.IsAggressive
            };
        }

        public void DrawStats(int position)
        {
            Game.DrawMonsterStats(this, position);
        }

        public virtual void SetBehavior() { }
    }
}
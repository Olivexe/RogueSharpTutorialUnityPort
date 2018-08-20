using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class Monster : Actor
    {
        public int? TurnsAlerted { get; set; }

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
                Gold            = anotherMonster.Gold,
                Health          = anotherMonster.Health,
                MaxHealth       = anotherMonster.MaxHealth,
                Name            = anotherMonster.Name,
                Speed           = anotherMonster.Speed,
                Symbol          = anotherMonster.Symbol
            };
        }

        public void DrawStats(int position)
        {
            game.DrawMonsterStats(this, position);
        }

        public virtual void PerformAction(CommandSystem commandSystem)
        {
            var behavior = new StandardMoveAndAttack();
            behavior.Act(this, commandSystem, game);
        }
    }
}
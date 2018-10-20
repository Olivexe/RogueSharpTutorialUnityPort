using RogueSharp;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class Fireball : Ability, ITargetable
    {
        private readonly int attack;
        private readonly int attackChance;
        private readonly int area;

        public Fireball(Game game, Actor parent, int attack, int attackChance, int area) : base (game, parent)
        {
            Name = "Fireball";
            TurnsToRefresh = 40;
            TurnsUntilRefreshed = 0;
            this.attack = attack;
            this.attackChance = attackChance;
            this.area = area;
        }

        protected override bool PerformAbility()
        {
            return game.TargetingSystem.SelectArea(this, area);
        }

        public void SelectTarget(Point target)
        {
            DungeonMap map  = game.World;

            game.MessageLog.Add($"{Owner.Name} casts a {Name}.");

            Actor fireballActor = new Actor (game)
            {
                AttackBase      = attack,
                AttackChanceBase= attackChance,
                Name        = Name
            };

            foreach (Cell cell in map.GetCellsInSquare(target.X, target.Y, area))
            {
                Monster monster = map.GetMonsterAt(cell.X, cell.Y);
                if (monster != null)
                {
                    Command.Attack(fireballActor, monster, true);
                }
            }
        }
    }
}
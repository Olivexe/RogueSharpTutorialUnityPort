using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class Monster : Actor
    {
        public int? TurnsAlerted { get; set; }

        public Monster(Game game) : base(game) { }

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
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class Monster : Actor
    {
        public int? TurnsAlerted    { get; set; }
        public bool IsAggressive    { get; set; }
        public bool IsBoss          { get; set; }

        public Monster(Game game) : base(game) { }

        public void DrawStats(int position)
        {
            Game.DrawMonsterStats(this, position);
        }

        public virtual void SetBehavior() { }
    }
}
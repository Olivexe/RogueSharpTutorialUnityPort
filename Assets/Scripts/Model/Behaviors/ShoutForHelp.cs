using RogueSharp;
using RogueSharpTutorial.Model.Interfaces;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class ShoutForHelp : IBehavior
    {
        public Actor        Parent  { get; private set; }
        public Game         Game    { get; private set; }
        public DungeonMap   World   { get; private set; }

        public void SetBehavior(Game game, Actor parent)
        {
            Game = game;
            Parent = parent;
            World = game.World;
        }

        public bool Act()
        {
            bool didShoutForHelp = false;

            foreach (var monsterLocation in World.GetMonsterLocations())
            {
                if (Parent.FieldOfView.IsInFov(monsterLocation.X, monsterLocation.Y))
                {
                    Monster alertedMonster = World.GetMonsterAt(monsterLocation.X, monsterLocation.Y);
                    if (!alertedMonster.TurnsAlerted.HasValue)
                    {
                        alertedMonster.TurnsAlerted = 1;
                        didShoutForHelp = true;
                    }
                }
            }

            if (didShoutForHelp)
            {
                Game.MessageLog.Add($"{Parent.Name} shouts for help!");
            }

            return didShoutForHelp;
        }
    }
}

using RogueSharp;
using RogueSharpTutorial.Model.Interfaces;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class ShoutForHelp : IBehavior
    {
        public bool Act(Actor actor, Game game)
        {
            bool didShoutForHelp = false;
            DungeonMap dungeonMap = game.World;
            FieldOfView monsterFov = new FieldOfView(dungeonMap);

            monsterFov.ComputeFov(actor.X, actor.Y, actor.Awareness, true);

            foreach (var monsterLocation in dungeonMap.GetMonsterLocations())
            {
                if (monsterFov.IsInFov(monsterLocation.X, monsterLocation.Y))
                {
                    Monster alertedMonster = dungeonMap.GetMonsterAt(monsterLocation.X, monsterLocation.Y);
                    if (!alertedMonster.TurnsAlerted.HasValue)
                    {
                        alertedMonster.TurnsAlerted = 1;
                        didShoutForHelp = true;
                    }
                }
            }

            if (didShoutForHelp)
            {
                game.MessageLog.Add($"{actor.Name} shouts for help!");
            }

            return didShoutForHelp;
        }
    }
}

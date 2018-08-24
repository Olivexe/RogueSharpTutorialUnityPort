using RogueSharp;
using RogueSharpTutorial.Model.Interfaces;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class RunAway : IBehavior
    {
        public bool Act(Actor monster, Game game)
        {
            DungeonMap dungeonMap = game.World;
            Player player = game.Player;

            // Set the cells the monster and player are on to walkable so the pathfinder doesn't bail early
            dungeonMap.SetIsWalkable(monster.X, monster.Y, true);
            dungeonMap.SetIsWalkable(player.X, player.Y, true);

            GoalMap goalMap = new GoalMap(dungeonMap);

            goalMap.AddGoal(player.X, player.Y, 0);

            Path path = null;

            try
            {
                path = goalMap.FindPathAvoidingGoals(monster.X, monster.Y);
            }
            catch (PathNotFoundException)
            {
                game.MessageLog.Add($"{monster.Name} cowers in fear");
            }


            // Reset the cell the monster and player are on  back to not walkable
            dungeonMap.SetIsWalkable(monster.X, monster.Y, false);
            dungeonMap.SetIsWalkable(player.X, player.Y, false);

            if (path != null)
            {
                try
                {
                    Command.Move(monster, path.StepForward().X, path.StepForward().Y);
                }
                catch (NoMoreStepsException)
                {
                    game.MessageLog.Add($"{monster.Name} cowers in fear");
                }
            }

            return true;
        }
    }
}

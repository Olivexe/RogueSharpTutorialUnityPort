using RogueSharp;
using RogueSharpTutorial.Model.Interfaces;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class RunAway : IBehavior
    {
        public Actor        Parent  { get; private set; }
        public Game         Game    { get; private set; }
        public DungeonMap   World   { get; private set; }

        private Player      player;
        private GoalMap     goalMap;
        private Path        path;

        public void SetBehavior(Game game, Actor parent)
        {
            Game        = game;
            Parent      = parent;
            World       = game.World;
            player      = game.Player;
            goalMap     = new GoalMap(World);
            path        = null;
        }

        public bool Act()
        {
            if (path != null)
            {
                StepForward();
            }
            else
            {
                // Set the cells the monster and player are on to walkable so the pathfinder doesn't bail early
                World.SetIsWalkable(Parent.X, Parent.Y, true);
                World.SetIsWalkable(player.X, player.Y, true);

                goalMap.AddGoal(player.X, player.Y, 0);

                try
                {
                    path = goalMap.FindPathAvoidingGoals(Parent.X, Parent.Y);
                }
                catch (PathNotFoundException)
                {
                    Game.MessageLog.Add($"{Parent.Name} cowers in fear");
                }

                // Reset the cell the monster and player are on  back to not walkable
                World.SetIsWalkable(Parent.X, Parent.Y, false);
                World.SetIsWalkable(player.X, player.Y, false);

                StepForward();
            }
            
            return true;
        }

        public void Reset()
        {
            path = null;
        }

        private void StepForward()
        {
            if (path != null)
            {
                try
                {
                    Command.Move(Parent, path.StepForward().X, path.StepForward().Y);
                }
                catch (NoMoreStepsException)
                {
                    Game.MessageLog.Add($"{Parent.Name} cowers in fear");
                    path = null;
                }
            }
        }
    }
}

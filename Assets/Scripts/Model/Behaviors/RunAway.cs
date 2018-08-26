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

        public void SetBehavior(Game game, Actor parent)
        {
            Game = game;
            Parent = parent;
            World = game.World;
        }

        public bool Act()
        {
            Player player = Game.Player;

            // Set the cells the monster and player are on to walkable so the pathfinder doesn't bail early
            World.SetIsWalkable(Parent.X, Parent.Y, true);
            World.SetIsWalkable(player.X, player.Y, true);

            GoalMap goalMap = new GoalMap(World);

            goalMap.AddGoal(player.X, player.Y, 0);

            Path path = null;

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

            if (path != null)
            {
                try
                {
                    Command.Move(Parent, path.StepForward().X, path.StepForward().Y);
                }
                catch (NoMoreStepsException)
                {
                    Game.MessageLog.Add($"{Parent.Name} cowers in fear");
                }
            }

            return true;
        }
    }
}

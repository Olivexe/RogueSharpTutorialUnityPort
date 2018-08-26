using RogueSharp;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class StandardMoveAndAttack : IBehavior
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
            Player player           = Game.Player;

            Monster monster = Parent as Monster;

            World.SetIsWalkable(monster.X, monster.Y, true);                // Before we find a path, make sure to make the monster and player Cells walkable
            World.SetIsWalkable(player.X, player.Y, true);

            PathFinder pathFinder = new PathFinder(World, 1d);
            Path path = null;

            try
            {
                path = pathFinder.ShortestPath(
                World.GetCell(monster.X, monster.Y),
                World.GetCell(player.X, player.Y));
            }
            catch (PathNotFoundException)
            {
                // The monster can see the player, but cannot find a path to him
                // This could be due to other monsters blocking the way
                // Add a message to the message log that the monster is waiting
                Game.MessageLog.Add(monster.Name + " waits for a turn.");
            }

            World.SetIsWalkable(monster.X, monster.Y, false);               // Don't forget to set the walkable status back to false
            World.SetIsWalkable(player.X, player.Y, false);

            if (path != null)                                               // In the case that there was a path, tell the CommandSystem to move the monster
            {
                try
                {
                    Command.Move(monster, path.StepForward());
                }
                catch (NoMoreStepsException)
                {
                    Game.MessageLog.Add($"{monster.Name} waits for a turn");
                }
            }

            return true;
        }
    }
}
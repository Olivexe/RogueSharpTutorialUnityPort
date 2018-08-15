using System.Collections.Generic;
using System.Linq;
using RogueSharp;
using RogueSharpTutorial.View;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public class DungeonMap : Map
    {
        private Game                game;
        private MapGenerator        mapGenerator;

        public  List<Rectangle>     Rooms;
        public  List<Door>          Doors           { get; set; }
        private List<Monster>       monsters;
        public  Stairs              StairsUp        { get; set; }
        public  Stairs              StairsDown      { get; set; }

        public DungeonMap (Game game)
        {
            this.game   = game;

            game.SchedulingSystem.Clear();                                                  // When going down a level, clear the move schedule

            Rooms       = new List<Rectangle>();
            monsters    = new List<Monster>();
            Doors       = new List<Door>();
        }

        /// <summary>
        /// Called by MapGenerator after we generate a new map to add the player to the map.
        /// </summary>
        /// <param name="player"></param>
        public void AddPlayer(Player player)
        {
            game.Player = player;
            SetIsWalkable(player.X, player.Y, false);
            game.SchedulingSystem.Add(player);
        }

        /// <summary>
        /// Add a monster to the map and add to list of Monsters.
        /// </summary>
        /// <param name="monster"></param>
        public void AddMonster(Monster monster)
        {
            monsters.Add(monster);
            
            SetIsWalkable(monster.X, monster.Y, false);                                         // After adding the monster to the map make sure to make the cell not walkable
            game.SchedulingSystem.Add(monster);
        }

        /// <summary>
        /// Remove a monster and set cell as walkable.
        /// </summary>
        /// <param name="monster"></param>
        public void RemoveMonster(Monster monster)
        {
            monsters.Remove(monster);            
            SetIsWalkable(monster.X, monster.Y, true);                                          // After removing the monster from the map, make sure the cell is walkable again
            game.SchedulingSystem.Remove(monster);
        }

        /// <summary>
        /// Get a monster or null at specific x and y position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Monster GetMonsterAt(int x, int y)
        {
            return monsters.FirstOrDefault(m => m.X == x && m.Y == y);
        }

        /// <summary>
        /// Return the door at the x,y position or null if one is not found.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Door GetDoor(int x, int y)
        {
            return Doors.SingleOrDefault(d => d.X == x && d.Y == y);
        }

        /// <summary>
        /// The actor opens the door located at the x,y position.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void OpenDoor(Actor actor, int x, int y)
        {
            Door door = GetDoor(x, y);

            if (door != null && !door.IsOpen)
            {
                door.IsOpen = true;
                var cell = GetCell(x, y);
                
                SetCellProperties(x, y, true, cell.IsWalkable, cell.IsExplored);                            // Once the door is opened it should be marked as transparent and no longer block field-of-view

                game.MessageLog.Add(actor.Name + " opened a door");
            }
        }

        /// <summary>
        /// The Draw method will be called each time the map is updated.
        /// It will send all of the symbols/colors for each cell to the graphics view
        /// </summary>
        public void Draw()
        {
            foreach (Cell cell in GetAllCells())
            {
                SetConsoleSymbolForCell(cell);
            }

            foreach (Door door in Doors)
            {
                door.Draw(this);
            }

            StairsUp.Draw(this);
            StairsDown.Draw(this);

            int i = -1;                                                                         // Keep an index so we know which position to draw monster stats at
                                                                                                // Start at -1 in case no monsters in view, then can clear the stat bars
            foreach (Monster monster in monsters)                                               // Iterate through each monster on the map and draw it after drawing the Cells
            {
                monster.Draw(this);

                if (IsInFov(monster.X, monster.Y))                                              // When the monster is in the field-of-view also draw their stats
                {
                    i++;

                    monster.DrawStats(i);                                                       // Pass in the index to DrawStats and increment it afterwards
                }
            }
            if (i == -1)
            {
                game.ClearMonsterStats();
            }
        }

        /// <summary>
        /// This method will be called any time we move the player to update field-of-view.
        /// </summary>
        public void UpdatePlayerFieldOfView(Player player)
        {
            ComputeFov(player.X, player.Y, player.Awareness, true);                             // Compute the field-of-view based on the player's location and awareness
            foreach (Cell cell in GetAllCells())                                                // Mark all cells in field-of-view as having been explored
            {
                if (IsInFov(cell.X, cell.Y))
                {
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }

        /// <summary>
        /// Returns true when able to place the Actor on the cell or false otherwise.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool SetActorPosition(Actor actor, int x, int y)
        {
            if (GetCell(x, y).IsWalkable)                                                       // Only allow actor placement if the cell is walkable
            {
                SetIsWalkable(actor.X, actor.Y, true);                                          // The cell the actor was previously on is now walkable
                actor.X = x;                                                                    // Update the actor's position
                actor.Y = y;
                SetIsWalkable(actor.X, actor.Y, false);                                         // The new cell the actor is on is now not walkable

                OpenDoor(actor, x, y);

                if (actor is Player)                                                            // Don't forget to update the field of view if we just repositioned the player
                {
                    UpdatePlayerFieldOfView(actor as Player);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if player is stairs so if can move down a level.
        /// </summary>
        /// <returns></returns>
        public bool CanMoveDownToNextLevel()
        {
            Player player = game.Player;
            return StairsDown.X == player.X && StairsDown.Y == player.Y;
        }

        /// <summary>
        /// Look for a random location in the room that is walkable.
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public Point GetRandomWalkableLocationInRoom(Rectangle room)
        {
            if (DoesRoomHaveWalkableSpace(room))
            {
                for (int i = 0; i < 100; i++)
                {
                    int x = Game.Random.Next(1, room.Width - 2) + room.X;
                    int y = Game.Random.Next(1, room.Height - 2) + room.Y;
                    if (IsWalkable(x, y))
                    {
                        return new Point(x, y);
                    }
                }
            }

            return Point.Zero;                                                                      // If we didn't find a walkable location in the room return Point.Zero
        }

        /// <summary>
        /// Iterate through each Cell in the room and return true if any are walkable.
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool DoesRoomHaveWalkableSpace(Rectangle room)
        {
            for (int x = 1; x <= room.Width - 2; x++)
            {
                for (int y = 1; y <= room.Height - 2; y++)
                {
                    if (IsWalkable(x + room.X, y + room.Y))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// A helper method for setting the IsWalkable property on a Cell.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="isWalkable"></param>
        public void SetIsWalkable(int x, int y, bool isWalkable)
        {
            Cell cell = GetCell(x, y) as Cell;
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
        }

        /// <summary>
        /// Set basic symbols for the map.
        /// </summary>
        /// <param name="cell"></param>
        private void SetConsoleSymbolForCell(Cell cell)
        {
            if (IsInFov(cell.X, cell.Y))                                                        // When a cell is currently in the field-of-view it should be drawn with lighter colors
            {
                if (cell.IsWalkable)                                                            // Choose the symbol to draw based on if the cell is walkable or not '.' for floor and '#' for walls
                {
                    game.SetMapCell(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, '.', cell.IsExplored);
                }
                else
                {
                    game.SetMapCell(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#', cell.IsExplored);
                }
            }
            else                                                                                // When a cell is outside of the field of view draw it with darker colors
            {
                if (cell.IsWalkable)
                {
                    game.SetMapCell(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, '.', cell.IsExplored);
                }
                else
                {
                    game.SetMapCell(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#', cell.IsExplored);
                }
            }
        }
    }
}
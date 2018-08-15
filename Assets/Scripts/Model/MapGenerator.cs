using System;
using System.Linq;
using System.Collections.Generic;
using RogueSharpTutorial.Controller;
using RogueSharp;
using RogueSharp.DiceNotation;

namespace RogueSharpTutorial.Model
{
    public class MapGenerator
    {
        private readonly int        width;
        private readonly int        height;
        private readonly int        maxRooms;
        private readonly int        roomMaxSize;
        private readonly int        roomMinSize;

        private readonly DungeonMap map;
        private readonly Game       game;

        /// <summary>
        /// Constructing a new MapGenerator requires the dimensions of the maps it will create as well as the sizes and maximum number of rooms.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="maxRooms"></param>
        /// <param name="roomMaxSize"></param>
        /// <param name="roomMinSize"></param>
        public MapGenerator(Game game, int width, int height, int maxRooms, int roomMaxSize, int roomMinSize, int mapLevel)
        {
            this.width          = width;
            this.height         = height;
            this.maxRooms       = maxRooms;
            this.roomMaxSize    = roomMaxSize;
            this.roomMinSize    = roomMinSize;
            this.game           = game;
            map                 = new DungeonMap(game);
        }

        /// <summary>
        /// Generate a new map that places rooms randomly.
        /// </summary>
        /// <returns></returns>
        public DungeonMap CreateMap()
        {          
            map.Initialize(width, height);                                                  // Set the properties of all cells to false

            for (int r = 0; r < maxRooms; r++)                                              // Try to place as many rooms as the specified maxRooms
            {
                int roomWidth = Game.Random.Next(roomMinSize, roomMaxSize);                 // Determine the size and position of the room randomly
                int roomHeight = Game.Random.Next(roomMinSize, roomMaxSize);
                int roomXPosition = Game.Random.Next(0, width - roomWidth - 1);
                int roomYPosition = Game.Random.Next(0, height - roomHeight - 1);

                var newRoom = new Rectangle(roomXPosition, roomYPosition, roomWidth, roomHeight);// All of our rooms can be represented as Rectangles
                bool newRoomIntersects = map.Rooms.Any(room => newRoom.Intersects(room));   // Check to see if the room rectangle intersects with any other rooms

                if (!newRoomIntersects)                                                     // As long as it doesn't intersect add it to the list of rooms
                {
                    map.Rooms.Add(newRoom);
                }
            }

            for (int r = 1; r < map.Rooms.Count; r++)                                       // Iterate through each room that was generated
            {                                                                               // Don't do anything with the first room, so start at r = 1 instead of r = 0               
                int previousRoomCenterX = map.Rooms[r - 1].Center.X;                        // For all remaing rooms get the center of the room and the previous room
                int previousRoomCenterY = map.Rooms[r - 1].Center.Y;
                int currentRoomCenterX = map.Rooms[r].Center.X;
                int currentRoomCenterY = map.Rooms[r].Center.Y;
               
                if (Game.Random.Next(1, 2) == 1)                                            // Give a 50/50 chance of which 'L' shaped connecting hallway to tunnel out
                {
                    CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY);
                    CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, currentRoomCenterX);
                }
                else
                {
                    CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
                    CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, currentRoomCenterY);
                }
            }

            foreach (Rectangle room in map.Rooms)                                           // Iterate through each room that we wanted placed call CreateRoom to make it
            {
                CreateRoom(room);
                CreateDoors(room);
            }

            CreateStairs();
            PlacePlayer(game);
            PlaceMonsters();

            return map;
        }

        /// <summary>
        /// Find the center of the first room that we created and place the Player there.
        /// </summary>
        private void PlacePlayer(Game game)
        {
            Player player = game.Player;
            if (player == null)
            {
                player = new Player(game);
            }

            player.X = map.Rooms[0].Center.X;
            player.Y = map.Rooms[0].Center.Y;

            map.AddPlayer(player);
        }

        /// <summary>
        /// Given a rectangular area on the map set the cell properties for that area to true.
        /// </summary>
        /// <param name="room"></param>
        private void CreateRoom(Rectangle room)
        {
            for (int x = room.Left + 1; x < room.Right; x++)
            {
                for (int y = room.Top + 1; y < room.Bottom; y++)
                {
                    map.SetCellProperties(x, y, true, true, false);
                }
            }
        }

        /// <summary>
        /// Given a room with hallways dug out check to see if doors can be placed.
        /// </summary>
        /// <param name="room"></param>
        private void CreateDoors(Rectangle room)
        {
            int xMin = room.Left;                                                           // The the boundries of the room
            int xMax = room.Right;
            int yMin = room.Top;
            int yMax = room.Bottom;

            List<ICell> borderCells = new List<ICell>();                                    // Put the rooms border cells into a list
            borderCells.AddRange(map.GetCellsAlongLine(xMin, yMin, xMax, yMin));
            borderCells.AddRange(map.GetCellsAlongLine(xMin, yMin, xMin, yMax));
            borderCells.AddRange(map.GetCellsAlongLine(xMin, yMax, xMax, yMax));
            borderCells.AddRange(map.GetCellsAlongLine(xMax, yMin, xMax, yMax));

            
            foreach (Cell cell in borderCells)                                              // Go through each of the rooms border cells and look for locations to place doors.
            {
                if (IsPotentialDoor(cell))
                {
                    map.SetCellProperties(cell.X, cell.Y, false, true);                    // A door must block field-of-view when it is closed.
                    map.Doors.Add(new Door(game)
                    {
                        X = cell.X,
                        Y = cell.Y,
                        IsOpen = false
                    });
                }
            }
        }

        /// <summary>
        /// Create stairs up in the first room created and stairs down in the last room created.
        /// </summary>
        private void CreateStairs()
        {
            map.StairsUp = new Stairs (game)
            {
                X = map.Rooms.First().Center.X + 1,
                Y = map.Rooms.First().Center.Y,
                IsUp = true
            };
            map.StairsDown = new Stairs (game)
            {
                X = map.Rooms.Last().Center.X,
                Y = map.Rooms.Last().Center.Y,
                IsUp = false
            };
        }

        /// <summary>
        /// Checks to see if a cell is a good candidate for placement of a door.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private bool IsPotentialDoor(Cell cell)
        {
            if (!cell.IsWalkable)                                                           // If the cell is not walkable then it is a wall and not a good place for a door
            {
                return false;
            }

            Cell right  = (Cell)map.GetCell(cell.X + 1, cell.Y);                            // Store references to all of the neighboring cells 
            Cell left   = (Cell)map.GetCell(cell.X - 1, cell.Y);
            Cell top    = (Cell)map.GetCell(cell.X, cell.Y - 1);
            Cell bottom = (Cell)map.GetCell(cell.X, cell.Y + 1);

            if (map.GetDoor(cell.X, cell.Y) != null ||
                map.GetDoor(right.X, right.Y) != null ||
                map.GetDoor(left.X, left.Y) != null ||
                map.GetDoor(top.X, top.Y) != null ||
                map.GetDoor(bottom.X, bottom.Y) != null)                                    // Make sure there is not already a door here
            {
                return false;
            }

            if (right.IsWalkable && left.IsWalkable && !top.IsWalkable && !bottom.IsWalkable)// This is a good place for a door on the left or right side of the room
            {
                return true;
            }

            if (!right.IsWalkable && !left.IsWalkable && top.IsWalkable && bottom.IsWalkable)// This is a good place for a door on the top or bottom of the room
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Carve a tunnel out of the map parallel to the x-axis.
        /// </summary>
        /// <param name="xStart"></param>
        /// <param name="xEnd"></param>
        /// <param name="yPosition"></param>
        private void CreateHorizontalTunnel(int xStart, int xEnd, int yPosition)
        {
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++)
            {
                map.SetCellProperties(x, yPosition, true, true);
            }
        }

        /// <summary>
        /// Carve a tunnel out of the map parallel to the y-axis.
        /// </summary>
        /// <param name="yStart"></param>
        /// <param name="yEnd"></param>
        /// <param name="xPosition"></param>
        private void CreateVerticalTunnel(int yStart, int yEnd, int xPosition)
        {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++)
            {
                map.SetCellProperties(xPosition, y, true, true);
            }
        }

        /// <summary>
        /// Make a chance to place 1 to 4 monsters in every room except the starting room with the player.
        /// </summary>
        private void PlaceMonsters()
        {
            foreach (var room in map.Rooms)
            {             
                if (Dice.Roll("1D10") < 7)                                                  // Each room has a 60% chance of having monsters
                {                  
                    var numberOfMonsters = Dice.Roll("1D4");                                // Generate between 1 and 4 monsters

                    for (int i = 1; i < numberOfMonsters; i++)                              // Not starting at zero. Player is in room zero.
                    {                       
                        Point randomRoomLocation = map.GetRandomWalkableLocationInRoom(room);// Find a random walkable location in the room to place the monster                       
                        
                        if (randomRoomLocation != Point.Zero)                               // It's possible that the room doesn't have space to place a monster
                        {                                                                   // In that case skip creating the monster                           
                            var monster = Kobold.Create(1, game);                           // Temporarily hard code this monster to be created at level 1
                            monster.X = randomRoomLocation.X;
                            monster.Y = randomRoomLocation.Y;
                            map.AddMonster(monster);
                        }
                    }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using RogueSharpTutorial.View;
using RogueSharpTutorial.Model;
using RogueSharpTutorial.Utilities;
using RogueSharp.Random;

namespace RogueSharpTutorial.Controller
{
    public class Game
    {
        public static IRandom           Random              { get; private set; }

        private UI_Main                 rootConsole;
        private InputKeyboard           inputControl;

        private static readonly int     mapWidth            = 80;
        private static readonly int     mapHeight           = 48;
        private bool                    renderRequired      = true;

        public  Player                  Player              { get; set; }
        public  bool                    PlayerDied          { get; private set; }
        public  MessageLog              MessageLog          { get; private set; }
        public  DungeonMap              World               { get; private set; }
        public  SchedulingSystem        SchedulingSystem    { get; private set; }
        public  TargetingSystem         TargetingSystem     { get; private set; }
        public  CommandSystem           CommandSystem       { get; private set; }

        public int                      mapLevel            = 1;


        public Game(UI_Main console)
        {
            int seed                = (int)DateTime.UtcNow.Ticks;
            Random                  = new DotNetRandom(seed);
            CommandSystem           = new CommandSystem(this);
            MessageLog              = new MessageLog(this);
            SchedulingSystem        = new SchedulingSystem(this);
            TargetingSystem         = new TargetingSystem(this);

            rootConsole = console;
            rootConsole.UpdateView  += OnUpdate;                         // Set up a handler for graphic engine Update event

            MessageLog.Add("The rogue arrives on level " + mapLevel + ".");
            MessageLog.Add("Level created with seed '" + seed + "'.");

            GenerateMap();
            rootConsole.SetPlayer(Player);
            World.UpdatePlayerFieldOfView(Player);

            Player.Item1 = new RevealMapScroll(this);
            Player.Item2 = new RevealMapScroll(this);

            Draw();
        }

        public void SetMapCell(int x,int y, Colors foreColor, Colors backColor, char symbol, bool isExplored)
        {
            rootConsole.UpdateMapCell(x, y, foreColor, backColor, symbol, isExplored);
        }

        public void SetMapCellBackground(int x, int y, Colors backColor)
        {
            rootConsole.UpdateBackgroundCell(x, y, backColor);
        }

        public void PostMessageLog(Queue<string> messages, Colors color)
        {
            rootConsole.PostMessageLog(messages, color);
        }

        public void DrawPlayerStats()
        {
            rootConsole.DrawPlayerStats();
        }

        public void DrawPlayerInventory()
        {
            rootConsole.DrawPlayerInventory();
        }

        public void DrawMonsterStats(Monster monster, int position)
        {
            rootConsole.DrawMonsterStats(monster, position);
        }

        public void ClearMonsterStats()
        {
            rootConsole.ClearMonsterStats();
        }

        public void ResolvePlayerDeath()
        {
            MessageLog.Add("Your final score is " + Player.TotalScore);
            MessageLog.Add("Press the ENTER key to start over.");
            PlayerDied = true;
            MessageLog.Draw();
        }

        private void OnUpdate(object sender, UpdateEventArgs e)
        {
            if (TargetingSystem.IsPlayerTargeting)
            {
                InputCommands command = rootConsole.GetUserCommand();
                TargetingSystem.HandleKey(command);
                renderRequired = true;
            }
            else if (CommandSystem.IsPlayerTurn)
            {
                CheckKeyboard();
            }
            else
            {
                CommandSystem.ActivateMonsters();
                renderRequired = true;
            }

            MessageLog.Draw();

            if (renderRequired)
            {
                Draw();
                renderRequired = false;
            }
        }

        private void GenerateMap()
        {
            MapGenerator mapGenerator = new MapGenerator(this, mapWidth, mapHeight, 20, 13, 7, mapLevel);
            World = mapGenerator.CreateMap();
            rootConsole.GenerateMap(World);
        }

        private void Draw()
        {
            World.Draw();
            Player.Draw(World);
            TargetingSystem.Draw();
            Player.DrawStats();
        }

        private void CheckKeyboard()
        {
            bool didPlayerAct = false;

            InputCommands command = rootConsole.GetUserCommand();

            if(PlayerDied)
            {
                if (command == InputCommands.EnterKey)
                {
                    StartOver();
                }
            }
            else if (CommandSystem.IsPlayerTurn)
            {
                switch (command)
                {
                    case InputCommands.UpLeft:
                        didPlayerAct = CommandSystem.MovePlayer(Direction.UpLeft);
                        break;
                    case InputCommands.Up:
                        didPlayerAct = CommandSystem.MovePlayer(Direction.Up);
                        break;
                    case InputCommands.UpRight:
                        didPlayerAct = CommandSystem.MovePlayer(Direction.UpRight);
                        break;
                    case InputCommands.Left:
                        didPlayerAct = CommandSystem.MovePlayer(Direction.Left);
                        break;
                    case InputCommands.Right:
                        didPlayerAct = CommandSystem.MovePlayer(Direction.Right);
                        break;
                    case InputCommands.DownLeft:
                        didPlayerAct = CommandSystem.MovePlayer(Direction.DownLeft);
                        break;
                    case InputCommands.Down:
                        didPlayerAct = CommandSystem.MovePlayer(Direction.Down);
                        break;
                    case InputCommands.DownRight:
                        didPlayerAct = CommandSystem.MovePlayer(Direction.DownRight);
                        break;
                    case InputCommands.QAbility:
                        didPlayerAct = Player.QAbility.Perform();
                        break;
                    case InputCommands.WAbility:
                        didPlayerAct = Player.WAbility.Perform();
                        break;
                    case InputCommands.EAbility:
                        didPlayerAct = Player.EAbility.Perform();
                        break;
                    case InputCommands.RAbility:
                        didPlayerAct = Player.RAbility.Perform();
                        break;
                    case InputCommands.Item1:
                        didPlayerAct = CommandSystem.UseItem(1, this);
                        break;
                    case InputCommands.Item2:
                        didPlayerAct = CommandSystem.UseItem(2, this);
                        break;
                    case InputCommands.Item3:
                        didPlayerAct = CommandSystem.UseItem(3, this);
                        break;
                    case InputCommands.Item4:
                        didPlayerAct = CommandSystem.UseItem(4, this);
                        break;
                    case InputCommands.StairsDown:
                        if (World.CanMoveDownToNextLevel())
                        {
                            MoveMapLevelDown();
                            didPlayerAct = true;
                        }
                        break;
                    case InputCommands.CloseGame:
                        rootConsole.CloseApplication();
                        break;
                    default:
                        break;
                }
                          
                if (didPlayerAct)
                {
                    renderRequired = true;
                    CommandSystem.EndPlayerTurn();
                }
            }
        }

        private void MoveMapLevelDown()
        {
            rootConsole.ClearMap();
            MapGenerator mapGenerator = new MapGenerator(this, mapWidth, mapHeight, 20, 13, 7, ++mapLevel);
            World = mapGenerator.CreateMap();
            rootConsole.GenerateMap(World);
            rootConsole.SetPlayer(Player);
            World.UpdatePlayerFieldOfView(Player);
            Draw();
            MessageLog = new MessageLog(this);
            CommandSystem = new CommandSystem(this);
        }

        private void StartOver()
        {
            PlayerDied = false;
            Player = null;
            mapLevel = 1;
            rootConsole.ClearMap();

            int seed = (int)DateTime.UtcNow.Ticks;

            MessageLog.Add("The rogue arrives on level " + mapLevel + ".");
            MessageLog.Add("Level created with seed '" + seed + "'.");

            GenerateMap();
            rootConsole.SetPlayer(Player);
            World.UpdatePlayerFieldOfView(Player);

            Player.Item1 = new RevealMapScroll(this);
            Player.Item2 = new RevealMapScroll(this);

            Draw();
        }
    }
}
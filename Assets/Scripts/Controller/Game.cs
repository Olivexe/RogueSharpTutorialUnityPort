using System;
using System.Collections.Generic;
using RogueSharpTutorial.View;
using RogueSharpTutorial.Model;
using RogueSharpTutorial.Utilities;
using RogueSharp.Random;
//using UnityEngine;

namespace RogueSharpTutorial.Controller
{
    public class Game
    {
        public static IRandom           Random              { get; private set; }

        private UI_Main                 rootConsole;
        private InputKeyboard           inputControl;

        public  CommandSystem           commandSystem;
        private static readonly int     mapWidth            = 80;
        private static readonly int     mapHeight           = 48;
        private bool                    renderRequired      = true;

        public  MessageLog              MessageLog          { get; set; }
        public  DungeonMap              World               { get; private set; }
        public  Player                  Player              { get; set; }
        public  SchedulingSystem        SchedulingSystem    { get; private set; }

        public int                      mapLevel            = 1;

        public Game(UI_Main console)
        {
            int seed                = (int)DateTime.UtcNow.Ticks;
            Random                  = new DotNetRandom(seed);
            commandSystem           = new CommandSystem(this);
            MessageLog              = new MessageLog(this);
            SchedulingSystem        = new SchedulingSystem();

            rootConsole             = console;
            rootConsole.UpdateView  += OnUpdate;                         // Set up a handler for graphic engine Update event

            MessageLog.Add("The rogue arrives on level "+ mapLevel);
            MessageLog.Add("Level created with seed '" + seed + "'");

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

        private void OnUpdate(object sender, UpdateEventArgs e)
        {
            CheckKeyboard();
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
            Player.DrawStats();
        }

        private void CheckKeyboard()
        {
            bool didPlayerAct = false;

            InputCommands command = rootConsole.GetUserCommand();

            if (commandSystem.IsPlayerTurn)
            {
                switch (command)
                {
                    case InputCommands.UpLeft:
                        didPlayerAct = commandSystem.MovePlayer(Direction.UpLeft);
                        break;
                    case InputCommands.Up:
                        didPlayerAct = commandSystem.MovePlayer(Direction.Up);
                        break;
                    case InputCommands.UpRight:
                        didPlayerAct = commandSystem.MovePlayer(Direction.UpRight);
                        break;
                    case InputCommands.Left:
                        didPlayerAct = commandSystem.MovePlayer(Direction.Left);
                        break;
                    case InputCommands.Right:
                        didPlayerAct = commandSystem.MovePlayer(Direction.Right);
                        break;
                    case InputCommands.DownLeft:
                        didPlayerAct = commandSystem.MovePlayer(Direction.DownLeft);
                        break;
                    case InputCommands.Down:
                        didPlayerAct = commandSystem.MovePlayer(Direction.Down);
                        break;
                    case InputCommands.DownRight:
                        didPlayerAct = commandSystem.MovePlayer(Direction.DownRight);
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
                        MoveMapLevelDown();
                        didPlayerAct = true;
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
                    commandSystem.EndPlayerTurn();
                }
            }
            else
            {
                commandSystem.ActivateMonsters();
                renderRequired = true;
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
            commandSystem = new CommandSystem(this);
        }
    }
}
using System;
using System.Collections.Generic;
using RogueSharpTutorial.View;
using RogueSharpTutorial.Model;
using RogueSharpTutorial.Utilities;
using RogueSharpTutorial.Model.Interfaces;
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
        public  bool                    IsPlayerTurn        { get; private set; }

        public int                      mapLevel            = 1;

        public Game(UI_Main console)
        {
            int seed                = (int)DateTime.UtcNow.Ticks;
            Random                  = new DotNetRandom(seed);
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
            World.SetMonsters();
            Command.SetGame(this);

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

        public void EndPlayerTurn()
        {
            Player.Tick();
            IsPlayerTurn = false;
        }

        public void ActivateMonsters()
        {
            IScheduleable scheduleable = SchedulingSystem.Get();

            if (scheduleable is Player)
            {
                IsPlayerTurn = true;
                SchedulingSystem.Add(Player);
            }
            else
            {
                if (PlayerDied)                                                 // If player was killed by a monster, stop moving monsters.
                {
                    return;
                }

                if (scheduleable is Monster)
                {
                    ((Monster)scheduleable).PerformAction(InputCommands.None);      // Use 'None' as the action of the monster as the behabior is determined in its behavior scripts
                    SchedulingSystem.Add(scheduleable);
                }

                ActivateMonsters();
            }
        }

        private void OnUpdate(object sender, UpdateEventArgs e)
        {
            if (TargetingSystem.IsPlayerTargeting)
            {
                InputCommands command = rootConsole.GetUserCommand();
                TargetingSystem.HandleKey(command);
                renderRequired = true;
            }
            else if (IsPlayerTurn)
            {
                CheckKeyboard();
            }
            else
            {
                ActivateMonsters();
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
            InputCommands command = rootConsole.GetUserCommand();

            if (command == InputCommands.CloseGame)     //Always check to close game first
            {
                rootConsole.CloseApplication();
                return;
            }
            else if (PlayerDied)
            {
                if (command == InputCommands.EnterKey)
                {
                    StartOver();
                }
            }
            else
            {
                if(command == InputCommands.ForgetAbility)
                {
                    if(Player.HaveAnyAbility())
                    {
                        rootConsole.OpenModalWindow(ModalWindowTypes.AbilityForget);
                        //InputLocked = true;
                    }
                    else
                    {
                        MessageLog.Add("You have no abilities to forget!");
                    }
                }
                else if (command == InputCommands.DropItem)
                {

                }
                else if (command == InputCommands.UseItem)
                {

                }
                else if (command == InputCommands.EquipItem)
                {

                }
                else if (command == InputCommands.GrabItem)
                {

                }
                else if (command == InputCommands.GrabAllItems)
                {

                }

                else if (command == InputCommands.StairsDown && World.CanMoveDownToNextLevel())
                {
                    if (World.stairsBlocked)
                    {
                        if (World.WhoIsBoss() != null)
                        {
                            MessageLog.Add($"The {World.WhoIsBoss().Name} is blocking the stairs down.");
                        }
                        else
                        {
                            MessageLog.Add($"Something is blocking the stairs down.");
                        }
                    }
                    else
                    {
                        MoveMapLevelDown();
                        renderRequired = true;
                        EndPlayerTurn();
                    }
                }
                else if(Player.PerformAction(command))
                {
                    renderRequired = true;
                    EndPlayerTurn();
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
            World.SetMonsters();
            Command.SetGame(this);
            Draw();
            MessageLog = new MessageLog(this);
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
            World.SetMonsters();
            Command.SetGame(this);

            Player.Item1 = new RevealMapScroll(this);
            Player.Item2 = new RevealMapScroll(this);

            Draw();
        }
    }
}
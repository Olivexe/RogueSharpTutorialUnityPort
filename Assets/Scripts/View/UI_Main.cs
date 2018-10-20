using System;
using System.Collections.Generic;
using UnityEngine;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Utilities;
using RogueSharpTutorial.Model;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.View
{
    [Serializable]
    public class UI_Main : MonoBehaviour
    {
        public event UpdateEventHandler         UpdateView;
        public event ItemActionEventHandler     ItemToManage;

        [SerializeField] private UI_Stats       uiStats;
        [SerializeField] private UI_Messages    uiMessages;
        [SerializeField] private UI_HUD         uiHUD;
        [SerializeField] private UI_Abilities   uiAbilities;
        [SerializeField] private UI_Inventory   uiInventory;
        [SerializeField] private UI_Profile     uiProfile;

        [SerializeField] private InputKeyboard  inputKeyboard;
        [SerializeField] private PlayerCamera   playerCameraScript;
        [SerializeField] private Camera         playerCamera;
        [SerializeField] private TileUnity      tilePrefab;

        [SerializeField] private GameObject     windowAbilities;
        [SerializeField] private GameObject     windowInventory;
        [SerializeField] private GameObject     windowProfile;

        public                   Game           Game                { get; private set; }
        private                  TileUnity[,]   mapObjects;

        private void Start()
        {
            uiStats         = GetComponent<UI_Stats>();
            uiMessages      = GetComponent<UI_Messages>();
            inputKeyboard   = GetComponent<InputKeyboard>();
            uiHUD           = GetComponent<UI_HUD>();
            uiAbilities     = GetComponent<UI_Abilities>();
            uiInventory     = GetComponent<UI_Inventory>();
            uiProfile       = GetComponent<UI_Profile>();

            Game = new Game(this);
        }

        private void Update()
        {
            UpdateGame();
        }

        private void UpdateGame()
        {
            UpdateView(this, new UpdateEventArgs(Time.time));
        }

        private bool IsInView(Vector3 position)
        {
            Vector3 pointOnScreen = playerCamera.WorldToScreenPoint(position);

            //Is in FOV
            if ((pointOnScreen.x < 0) || (pointOnScreen.x > Screen.width) || (pointOnScreen.y < 0) || (pointOnScreen.y > Screen.height))
            {
                return false;
            }

            return true;
        }

        public void SetPlayer(Player player)
        {
            playerCameraScript.InitCamera(player);
        }

        /// <summary>
        /// Get the current value of the user input command
        /// </summary>
        /// <returns></returns>
        public InputCommands GetUserCommand()
        {
            return inputKeyboard.Command;
        }

        public void ManageItem(IInventory itemSelected, ItemActionType action)
        {
            ItemToManage(this, new ItemActionEventArgs(itemSelected, action));
        }

        public void GenerateMap(DungeonMap map)
        {
            mapObjects = new TileUnity[map.Width, map.Height];
        }

        public void ClearMap()
        {
            for (int x = 0; x < mapObjects.GetLength(0); x++)
            {
                for (int y = 0; y < mapObjects.GetLength(1); y++)
                {
                    if (mapObjects[x, y] != null)
                    {
                        mapObjects[x, y].TileActive = false;
                        mapObjects[x, y].ReturnToPool();
                        mapObjects[x, y] = null;
                    }
                }
            }
        }

        public void UpdateMapCell(int x, int y, Colors foreColor, Colors backColor, char symbol, bool isExplored)
        {
            TileUnity tile;

            if (mapObjects[x, y] != null)
            {
                tile = mapObjects[x, y];
            }
            else
            {
                tile = null;
            }

            if (!IsInView(new Vector3(x, y, 0)) || !isExplored)
            {
                if (tile != null)
                {
                    mapObjects[x, y] = null;
                    tile.TileActive = false;
                    tile.ReturnToPool();
                }
            }
            else
            {
                if (tile == null)
                {
                    tile = tilePrefab.GetPooledInstance<TileUnity>();
                    tile.transform.position = new Vector3(x, y, 0);
                    tile.IsAsciiTile = true;
                    mapObjects[x, y] = tile;
                }

                tile.TileActive = isExplored;
                tile.BackgroundColor = ColorMap.UnityColors[backColor];
                tile.Text = symbol;
                tile.TextColor = ColorMap.UnityColors[foreColor];
            }
        }

        public void UpdateBackgroundCell(int x, int y, Colors backColor)
        {
            if (x >= 0 && x < mapObjects.GetLength(0) && y >= 0 && y < mapObjects.GetLength(1) && mapObjects[x, y] != null )
            {
                mapObjects[x, y].BackgroundColor = ColorMap.UnityColors[backColor];
            }
        }

        public void PostMessageLog(Queue<string> messages, Colors color)
        {
            uiMessages.PostMessageLog(messages, ColorMap.UnityColors[color]);
        }

        public void DrawPlayerStats()
        {
            uiStats.DrawPlayerStats(Game);
        }

        public void DrawPlayerInventory()
        {
            uiHUD.DrawPlayerInventory(Game);
        }

        public void DrawMonsterStats(Monster monster, int position)
        {
            uiStats.DrawMonsterStats(monster, position);
        }

        public void ClearMonsterStats()
        {
            uiStats.ClearMonsterStats();
        }

        public void OpenModalWindow(ModalWindowTypes task)
        {
            inputKeyboard.CurrentWindow = task;

            switch(task)
            {
                case ModalWindowTypes.Abilities:
                    windowAbilities.SetActive(true);
                    break;
                case ModalWindowTypes.Inventory:
                    windowInventory.SetActive(true);
                    break;
                case ModalWindowTypes.Profile:
                    windowProfile.SetActive(true);
                    break;
                case ModalWindowTypes.ExploreMap:
                    Game.MessageLog.Add("Exploring Map. Hit Esc to resume game.");
                    SetCameraExplore(true);
                    break;
                default:
                    break;
            }
        }

        public void CloseAllModalWindows()
        {
            windowAbilities.SetActive(false);
            windowInventory.SetActive(false);
            windowProfile.SetActive(false);
            inputKeyboard.CurrentWindow = ModalWindowTypes.Primary;
        }

        public void SetCameraExplore (bool isExploring)
        {
            playerCameraScript.SetCameraExploreMode(isExploring);
            Game.MapExploringMode = isExploring;
        }

        public void CloseApplication()
        {
            Application.Quit();
        }
    }
}
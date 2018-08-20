using System;
using System.Collections.Generic;
using UnityEngine;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Utilities;
using RogueSharpTutorial.Model;

namespace RogueSharpTutorial.View
{
    [System.Serializable]
    public class UI_Main : MonoBehaviour
    {
        public event UpdateEventHandler         UpdateView;

        [SerializeField] private UI_Stats       uiStats;
        [SerializeField] private UI_Messages    uiMessages;
        [SerializeField] private UI_Inventory   uiInventory;
        [SerializeField] private InputKeyboard  inputKeyboard;
        [SerializeField] private PlayerCamera   playerCamera;
        [SerializeField] private TileUnity      tilePrefab;

        private                 Game            game;
        private                 TileUnity[,]    mapObjects;

        private void Start()
        {
            uiStats         = GetComponent<UI_Stats>();
            uiMessages      = GetComponent<UI_Messages>();
            inputKeyboard   = GetComponent<InputKeyboard>();
            uiInventory     = GetComponent<UI_Inventory>();

            game            = new Game(this);
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
            Vector3 pointOnScreen = Camera.main.WorldToScreenPoint(position);

            //Is in FOV
            if ((pointOnScreen.x < 0) || (pointOnScreen.x > Screen.width) || (pointOnScreen.y < 0) || (pointOnScreen.y > Screen.height))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get the current value of the user input command
        /// </summary>
        /// <returns></returns>
        public InputCommands GetUserCommand()
        {
            return inputKeyboard.Command;
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

            if (!IsInView(new Vector3(x, y, 0)))
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

        public void PostMessageLog(Queue<string> messages, Colors color)
        {
            uiMessages.PostMessageLog(messages, ColorMap.UnityColors[color]);
        }

        public void DrawPlayerStats()
        {
            uiStats.DrawPlayerStats(game);
        }

        public void DrawPlayerInventory()
        {
            uiInventory.DrawPlayerInventory(game);
        }

        public void DrawMonsterStats(Monster monster, int position)
        {
            uiStats.DrawMonsterStats(monster, position);
        }

        public void ClearMonsterStats()
        {
            uiStats.ClearMonsterStats();
        }

        public void SetPlayer(Player player)
        {
            playerCamera.InitCamera(player);
        }

        public void CloseApplication()
        {
            Application.Quit();
        }
    }
}
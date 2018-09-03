using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model;

namespace RogueSharpTutorial.View
{
    public class UI_Map : MonoBehaviour
    {
        [SerializeField] private UI_Main        rootConsole;
        [SerializeField] private PlayerCamera   playerCamera;
        [SerializeField] private float          moveSpeed       = .2f;

        public InputCommands GetInput()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                rootConsole.CloseAllModalWindows();
                rootConsole.Game.MessageLog.Add("Resuming game.");
                rootConsole.SetCameraExplore(false);
                return InputCommands.None;
            }
            else if (Input.GetKey(KeyCode.Keypad8) || Input.GetKey(KeyCode.UpArrow))
            {
                playerCamera.SetPosition(0, moveSpeed);
            }
            else if (Input.GetKey(KeyCode.Keypad4) || Input.GetKey(KeyCode.LeftArrow))
            {
                playerCamera.SetPosition(-moveSpeed, 0);
            }
            else if (Input.GetKey(KeyCode.Keypad6) || Input.GetKey(KeyCode.RightArrow))
            {
                playerCamera.SetPosition(moveSpeed, 0);
            }
            else if (Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.DownArrow))
            {
                playerCamera.SetPosition(0, -moveSpeed);
            }

            return InputCommands.None;
        }
    }
}
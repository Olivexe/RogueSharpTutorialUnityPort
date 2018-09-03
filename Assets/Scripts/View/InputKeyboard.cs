using UnityEngine;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.View
{
    public class InputKeyboard : MonoBehaviour
    {
        [SerializeField] private    UI_Main         rootConsole;
        [SerializeField] private    UI_Abilities    ui_Abilities;
        [SerializeField] private    UI_Inventory    ui_Inventory;
        [SerializeField] private    UI_Profile      ui_Profile;
        [SerializeField] private    UI_HUD          ui_HUD;
        [SerializeField] private    UI_Map          ui_Map;

        private InputCommands                       input;
        
        public  ModalWindowTypes                    CurrentWindow       { get; set; }

        /// <summary>
        /// Will return the last keyboard up pressed. Will then clear the input to None.
        /// </summary>
        public  InputCommands Command
        {
            get
            {
                InputCommands returnVal = input;
                input = InputCommands.None;
                return returnVal;
            }
        }

        private void Update()
        {
            input = GetKeyboardValue();
        }

        private InputCommands GetKeyboardValue()
        {
            switch(CurrentWindow)
            {
                case ModalWindowTypes.Primary:
                    return GetPrimaryWindowKey();
                case ModalWindowTypes.Abilities:
                    return ui_Abilities.GetInput();
                case ModalWindowTypes.Inventory:
                    return ui_Inventory.GetInput();
                case ModalWindowTypes.Profile:
                    return ui_Profile.GetInput();
                case ModalWindowTypes.ExploreMap:
                    return ui_Map.GetInput();
                default:
                    return InputCommands.None;
            }
        }

        private InputCommands GetPrimaryWindowKey()
        {
            if (Input.GetKeyUp(KeyCode.Keypad7))
            {
                return InputCommands.UpLeft;
            }
            else if (Input.GetKeyUp(KeyCode.Keypad8) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                return InputCommands.Up;
            }
            else if (Input.GetKeyUp(KeyCode.Keypad9))
            {
                return InputCommands.UpRight;
            }
            else if (Input.GetKeyUp(KeyCode.Keypad4) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                return InputCommands.Left;
            }
            else if (Input.GetKeyUp(KeyCode.Keypad6) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                return InputCommands.Right;
            }
            else if (Input.GetKeyUp(KeyCode.Keypad1))
            {
                return InputCommands.DownLeft;
            }
            else if (Input.GetKeyUp(KeyCode.Keypad2) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                return InputCommands.Down;
            }
            else if (Input.GetKeyUp(KeyCode.Keypad3))
            {
                return InputCommands.DownRight;
            }
            else if (Input.GetKeyUp(KeyCode.Period))
            {
                return InputCommands.StairsDown;
            }
            else if (Input.GetKeyUp(KeyCode.Comma))
            {
                return InputCommands.StairsUp;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                return InputCommands.Item1;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                return InputCommands.Item2;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                return InputCommands.Item3;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                return InputCommands.Item4;
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                return InputCommands.QAbility;
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                return InputCommands.WAbility;
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                return InputCommands.EAbility;
            }
            else if (Input.GetKeyUp(KeyCode.R))
            {
                return InputCommands.RAbility;
            }
            else if (Input.GetKeyUp(KeyCode.Escape))
            {
                return InputCommands.CloseGame;
            }
            else if (Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Return))
            {
                return InputCommands.EnterKey;
            }
            else if (Input.GetKeyUp(KeyCode.Y))
            {
                rootConsole.OpenModalWindow(ModalWindowTypes.Abilities);
            }
            else if (Input.GetKeyUp(KeyCode.I))
            {
                rootConsole.OpenModalWindow(ModalWindowTypes.Inventory);
            }
            else if (Input.GetKeyUp(KeyCode.P))
            {
                rootConsole.OpenModalWindow(ModalWindowTypes.Profile);
            }
            else if (Input.GetKeyUp(KeyCode.G))
            {
                return InputCommands.GrabAllItems;
            }
            else if (Input.GetKeyUp(KeyCode.M))
            {
                rootConsole.OpenModalWindow(ModalWindowTypes.ExploreMap);
                return InputCommands.MapExploreOn;
            }
            else if (Input.GetKeyUp(KeyCode.Z))
            {
                if (ui_HUD.Isitem1Onground)
                {
                    return InputCommands.GrabItemZ;
                }
            }
            else if (Input.GetKeyUp(KeyCode.X))
            {
                if (ui_HUD.Isitem2Onground)
                {
                    return InputCommands.GrabItemX;
                }
            }
            else if (Input.GetKeyUp(KeyCode.C))
            {
                if (ui_HUD.Isitem3Onground)
                {
                    return InputCommands.GrabItemC;
                }
            }
            else if (Input.GetKeyUp(KeyCode.V))
            {
                if (ui_HUD.Isitem4Onground)
                {
                    return InputCommands.GrabItemV;
                }
            }

            return InputCommands.None;
        }
    }
}